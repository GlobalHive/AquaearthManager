using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : Singleton<Manager>
{
    // Main Canvas
    [FoldoutGroup("Main Canvas References"), SerializeField, SceneObjectsOnly, InlineEditor(InlineEditorModes.GUIOnly)]
    CanvasScaler _CanvasScaler;
    [FoldoutGroup("Main Canvas References"), SceneObjectsOnly]
    public GlobalHive.UI.ModernUI.ModalWindowTabs Tabs;

    // Item
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    Button _SellButton, _ClearSelectionButton;
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    GameObject _ItemTemplate;
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    Transform _ItemArray;
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    TMP_Text _PaginationText;

    // Sales
    [TabGroup("Sales"), SerializeField, SceneObjectsOnly]
    GameObject _SalesTemplate;
    [TabGroup("Sales"), SerializeField, SceneObjectsOnly]
    Transform _SalesArray;
    [TabGroup("Sales"), SerializeField, SceneObjectsOnly]
    TMP_Text _SalePaginationText;

    int _OpenCategory = 0;

    public List<int> selectedItems = new List<int>();
    int maxPage = 0;
    int currentPage = 0;

    public int MaxPage {
        get { return maxPage/100; }
        set {
            maxPage = value;
            if (currentPage > MaxPage)
                currentPage = 0;
        }
    }

    private void Start() {

#if UNITY_STANDALONE_WIN  // Setzt die mindest fenster grösse fest (Windows Only)
        GlobalHive.Windows.MinimumWindowSize.Set(964, 592);
#endif

        // Datenbank verbindungen starten
        GlobalHive.DatabaseAPI.API.GetInstance();

        // Startet das laden der Kategorien
        LoadCategories();
        
        // Setzt die klick funktion fest für den verkauf knopf (Gibt die liste mit)
        _SellButton.onClick.AddListener(() => ItemSeller.Instance.OpenItemSeller(selectedItems));
    }

    private void Update() {
        if (Input.touchCount == 2) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            _CanvasScaler.scaleFactor = Mathf.Clamp(_CanvasScaler.scaleFactor + (difference * 0.001f), 1f, 1.4f);
        }
    }

    public async Task<int> GetItemCountAsync(string table, int category = 0) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd;

        if (category != 0)
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM {table} WHERE category = '{category}'", conn);
        else
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM {table}", conn);

        object result = await cmd.ExecuteScalarAsync();
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return int.Parse(result.ToString());
    }

    #region Category
    public async void LoadCategories() {
        LoadingScreen.Instance.ShowLoadingScreen("Lade Kategorie"); // Ladebild sichtbar machen

        var categoryProgress = new Progress<string>();
        categoryProgress.ProgressChanged += CategoryProgress_ProgressChanged;

        List<Category> tempCategories = await Task.Run(() => GetCategoriesAsync(categoryProgress)); // Startet das asynchrone laden der kategorien

        ObjectPooler.Instance.HidePooledObjects("CategoryElement"); // Macht alle pooling objekte unsichbar

        foreach (Category category in tempCategories) {
            GameObject tempObject = ObjectPooler.Instance.GetPooledObject("CategoryElement"); // Nimmt ein element aus dem "pool"
            RawImage tempRawImage = tempObject.transform.Find("Background/Image").GetComponent<RawImage>(); // Sucht das bild im template
            TMP_Text tempTitle = tempObject.transform.Find("Background/TextArea/Title").GetComponent<TMP_Text>(); // Sucht den titel im template
            Button tempButton = tempObject.GetComponent<Button>(); // Sucht den "button" im template

            Texture2D tempImage = category.Image.ToTexture(); // Konvertiert die Bytes[] zu eines textur

            tempTitle.SetText(category.Name); // Setzt den name als titel
            if (tempImage != null)
                tempRawImage.texture = tempImage; // Setzt die textur als bild, falls vorhanden

            tempButton.onClick.RemoveAllListeners();
            tempButton.onClick.AddListener(() => CategoryButtonClick(category.ID)); // Setzt die funktion beim clicken des elements
            tempObject.SetActive(true); // Macht das template sichtbar

        }

        categoryProgress.ProgressChanged -= CategoryProgress_ProgressChanged;
        categoryProgress = null;
        LoadingScreen.Instance.HideLoadingScreen(); // Ladebild unsichbar machen
    }

    private void CategoryProgress_ProgressChanged(object sender, string e) {
        LoadingScreen.Instance.SetText($"Lade Kategorie\n{e}");
    }

    public void CategoryButtonClick(int categoryID) {
        _OpenCategory = categoryID; // Setzt die aktuell geöffnete kategorie fest
        LoadInventory(categoryID);
    }

    public async Task<List<Category>> GetCategoriesAsync(IProgress<string> progress=null) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync(); // Erstellt eine verbindung mit der Datenbank
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM categorys", conn); // Datenbank befehl

        List<Category> tempCategories = new List<Category>();

        using (DbDataReader reader = await cmd.ExecuteReaderAsync()) { // Liest alle daten aus der Datenbank ( Wie definiert im befehl)
            while (await reader.ReadAsync()) { // während der "reader" noch daten hat, loopen

                
                Category tempCat = new Category { // Erstellt eine Kategorie
                    ID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Image = null
                };

                if (!reader.IsDBNull(2)) {
                    byte[] imageBytes = reader.GetFieldValue<byte[]>(2);

                    if (imageBytes.Length != 0)
                        tempCat.Image = imageBytes;
                }
                    
                tempCategories.Add(tempCat);
                if (progress != null) {
                    progress.Report(tempCat.Name);
                }
            }
        }
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempCategories;
    }

    public async Task<Category> GetCategoryAsync(int id, IProgress<string> progress = null) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM categorys WHERE id = '{id}'", conn);

        DbDataReader reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        Category tempCategory = new Category {
            ID = reader.GetInt32(0),
            Image = null,
            Name = reader.GetString(1)
        };
        if (progress != null)
            progress.Report(tempCategory.Name);
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempCategory;
    }
    #endregion

    #region Inventory
    public async void LoadInventory(int category) {
        LoadingScreen.Instance.ShowLoadingScreen("Lade Artikel");
        if (category == -1)
            category = _OpenCategory;

        List<Item> tempItems = new List<Item>();

        MaxPage = await GetItemCountAsync("items", category);
        if (currentPage > maxPage)
            currentPage = 0;
        _PaginationText.SetText($"{currentPage + 1} / {MaxPage + 1}");

        var itemProgress = new Progress<string>();
        itemProgress.ProgressChanged += ItemProgress_ProgressChanged;

        if (category != 0)
            tempItems = await Task.Run(() => GetItemsAsync(new MySqlCommand($"SELECT * FROM items WHERE category = '{category}' ORDER BY name ASC LIMIT {currentPage * 100}, {currentPage + 1 * 100}"), itemProgress));
        else
            tempItems = await Task.Run(()=> GetItemsAsync(new MySqlCommand($"SELECT * FROM items ORDER BY name ASC LIMIT {currentPage * 100}, {currentPage + 1 * 100}"), itemProgress));

        ObjectPooler.Instance.HidePooledObjects("InventoryElement");

        foreach (Item tempItem in tempItems) {
            GameObject tempObject = ObjectPooler.Instance.GetPooledObject("InventoryElement");
            RawImage tempRawImage = tempObject.transform.Find("Image").GetComponent<RawImage>();
            TMP_Text tempTitle = tempObject.transform.Find("Name").GetComponent<TMP_Text>();
            TMP_Text tempDescription = tempObject.transform.Find("Description").GetComponent<TMP_Text>();
            Button tempButton = tempObject.transform.Find("btnEdit").GetComponent<Button>();
            SelectableObject tempSelectableObject = tempObject.GetComponent<SelectableObject>();

            Texture2D tempImage = tempItem.Image.ToTexture();

            if (tempImage != null)
                tempRawImage.texture = tempImage;

            tempTitle.SetText(tempItem.Name);
            tempDescription.SetText($"{tempItem.Category.Name} – {tempItem.Amount}x – CHF {tempItem.Price.ToString("N2")}");
            tempButton.onClick.RemoveAllListeners();
            tempButton.onClick.AddListener(() => ItemEditor.Instance.OpenItemEditor(tempItem.ID));

            tempSelectableObject.OnSelectionChanged.RemoveAllListeners();
            tempSelectableObject.OnSelectionChanged.AddListener((s) => OnSelectionChanged(tempItem.ID, s));

            tempObject.SetActive(true);

            if (selectedItems.Contains(tempItem.ID))
                tempSelectableObject.SetSelected(true, true);
            else {
                tempSelectableObject.SetSelected(false, true);
                tempSelectableObject.OnPointerExit(null);
            }
                
        }

        itemProgress.ProgressChanged -= ItemProgress_ProgressChanged;
        itemProgress = null;

        _SellButton.interactable = selectedItems.Count > 0;
        _ClearSelectionButton.interactable = _SellButton.interactable;
        Tabs.PanelAnim(1);
        LoadingScreen.Instance.HideLoadingScreen();
    }

    private void ItemProgress_ProgressChanged(object sender, string e) {
        LoadingScreen.Instance.SetText($"Lade Artikel\n{e}");
    }

    public async Task<List<Item>> GetItemsAsync(MySqlCommand cmd, IProgress<string> progress = null) {
        List<Item> tempItemList = new List<Item>();

        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        cmd.Connection = conn;
        using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
            while (await reader.ReadAsync()) {
                Item tempItem = new Item {
                    ID = reader.GetInt32(0),
                    Image = null,
                    Name = reader.GetString(1),
                    Amount = reader.GetInt32(3),
                    Price = reader.GetDouble(4)
                };

                if (!reader.IsDBNull(2)) {
                    tempItem.Image = reader.GetFieldValue<byte[]>(2);
                }

                tempItem.Category = await GetCategoryAsync(reader.GetInt32(5));
                tempItemList.Add(tempItem);

                if (progress != null) {
                    progress.Report(tempItem.Name);
                }
            }
        }
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempItemList;
    }

    public async Task<Item> GetItemAsync(int itemID, IProgress<string> progress = null) {
        Item tempItem = null;

        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM items WHERE id = '{itemID}'", conn);
        using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
            while (await reader.ReadAsync()) {

                tempItem = new Item {
                    ID = reader.GetInt32(0),
                    Image = null,
                    Name = reader.GetString(1),
                    Amount = reader.GetInt32(3),
                    Price = reader.GetDouble(4)
                };

                if (progress != null)
                    progress.Report(tempItem.Name);

                if (!reader.IsDBNull(2)) {
                    tempItem.Image = reader.GetFieldValue<byte[]>(2);
                }

                tempItem.Category = await GetCategoryAsync(reader.GetInt32(5));
            }
        }
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempItem;
    }

    public async Task<double> GetItemPrice(int itemID) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"SELECT price FROM items WHERE id = '{itemID}'", conn);
        object obj = await cmd.ExecuteScalarAsync();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return (double)obj;
    }

    public void OnSelectionChanged(int itemID, bool isSelected) {
        if (isSelected) {
            selectedItems.Add(itemID);
        }
        else {
            selectedItems.Remove(itemID);
        }

        _SellButton.interactable = selectedItems.Count > 0;
        _ClearSelectionButton.interactable = _SellButton.interactable;
    }

    public void SetPagination(bool increase) {
        if (increase) {
            if (currentPage == maxPage)
                currentPage = 0;
            else
                currentPage++;
        }
        else {
            if (currentPage == 0)
                currentPage = maxPage;
            else
                currentPage--;
        }

        _PaginationText.SetText($"{currentPage + 1} / {MaxPage + 1}");
        LoadInventory(-1);
    }

    public void ClearSelectedItems() {
        selectedItems.Clear();
        _SellButton.interactable = false;
        _ClearSelectionButton.interactable = false;
        LoadInventory(-1);
    }
    #endregion

    #region Sales
    public async void LoadSales() {

        LoadingScreen.Instance.ShowLoadingScreen();

        MaxPage = await GetItemCountAsync("sales");
        if (currentPage > maxPage)
            currentPage = 0;
        _SalePaginationText.SetText($"{currentPage + 1} / {MaxPage + 1}");

        var salesProgress = new Progress<string>();
        salesProgress.ProgressChanged += SalesProgress_ProgressChanged;
        
        Dictionary<int, Sales> _Sales = await Task.Run(()=> GetSalesAsync(salesProgress));

        ObjectPooler.Instance.HidePooledObjects("SalesElement");
        foreach (Sales item in _Sales.Values) {
            double currentPrice = 0;

            GameObject go = ObjectPooler.Instance.GetPooledObject("SalesElement");
            go.transform.Find("Content/Title").GetComponent<TMP_Text>().SetText(item.SoldDateTime.ToString("dd/MM/yyyy HH:mm"));
            go.transform.Find("Content/Artikel").GetComponent<TMP_Text>().SetText($"{item.SoldItemsList.Count} Artikel");

            foreach (Sales.SoldItems si in item.SoldItemsList) {
                currentPrice += await GetItemPrice(si.ItemID) * si.Amount;
            }
            item.FullPrice = currentPrice;

            go.transform.Find("Content/Price").GetComponent<TMP_Text>().SetText($"CHF {currentPrice.ToString("N2")}");
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate () {
                SoldItem.Instance.ShowSoldItem(item.SaleID);
            });
            go.SetActive(true);
        }

        salesProgress.ProgressChanged -= SalesProgress_ProgressChanged;
        salesProgress = null;
        LoadingScreen.Instance.HideLoadingScreen();
    }

    private void SalesProgress_ProgressChanged(object sender, string e) {
        LoadingScreen.Instance.SetText($"Lade Verkauf\n{e}");
    }

    public async Task<Dictionary<int,Sales>> GetSalesAsync(IProgress<string> progress = null) {
        List<DatabaseSales> dbs = new List<DatabaseSales>();
        Dictionary<int, Sales> sales = new Dictionary<int, Sales>();
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM sales ORDER BY date ASC LIMIT {currentPage * 100}, {currentPage + 1 * 100}", conn);

        using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
            while (await reader.ReadAsync()) {

                DatabaseSales databaseSales = new DatabaseSales {
                    ID = reader.GetInt32(0),
                    SalesID = reader.GetInt32(1),
                    ItemID = reader.GetInt32(2),
                    Amount = reader.GetInt32(3),
                    DateTime = reader.GetDateTime(4)
                };
                dbs.Add(databaseSales);
                if (progress != null) {
                    Item i = await GetItemAsync(databaseSales.ItemID);
                    progress.Report(i.Name);
                }
            }
        }

        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);

        foreach (DatabaseSales item in dbs) {
            if (sales.ContainsKey(item.SalesID)) {
                sales[item.SalesID].SoldItemsList.Add(new Sales.SoldItems {
                    ItemID = item.ItemID,
                    Amount = item.Amount
                });
            }
            else {
                Sales s = new Sales();
                s.SaleID = item.SalesID;
                s.SoldItemsList = new List<Sales.SoldItems>();
                s.SoldItemsList.Add(new Sales.SoldItems { ItemID = item.ItemID, Amount = item.Amount });
                s.SoldDateTime = item.DateTime;

                sales.Add(item.SalesID, s);
            }
        }

        return sales;
    }

    public async Task<Sales> GetSoldItemAsync(int saleID, IProgress<string> progress = null) {
        Sales tempSale = new Sales();
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM sales WHERE sale_id = '{saleID}'", conn);
        DatabaseSales dbs = null;

        tempSale.SoldItemsList = new List<Sales.SoldItems>();
        using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
            while (await reader.ReadAsync()) {
                dbs = new DatabaseSales {
                    ID = reader.GetInt32(0),
                    SalesID = reader.GetInt32(1),
                    ItemID = reader.GetInt32(2),
                    Amount = reader.GetInt32(3),
                    DateTime = reader.GetDateTime(4)
                };
                tempSale.SoldItemsList.Add(new Sales.SoldItems { 
                    ItemID = dbs.ItemID,
                    Amount = dbs.Amount
                });
                tempSale.FullPrice += await GetItemPrice(dbs.ItemID) * dbs.Amount;
                if (progress != null) {
                    progress.Report(dbs.DateTime.ToString("dd/MM/yyyy HH:mm"));
                }
            }
        }
        tempSale.SaleID = dbs.SalesID;
        tempSale.SoldDateTime = dbs.DateTime;
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempSale;
    }

    public void SetSalePagination(bool increase) {
        if (increase) {
            if (currentPage == maxPage)
                currentPage = 0;
            else
                currentPage++;
        }
        else {
            if (currentPage == 0)
                currentPage = maxPage;
            else
                currentPage--;
        }

        _SalePaginationText.SetText($"{currentPage + 1} / {MaxPage + 1}");
        LoadSales();
    }
    #endregion

    public void OnApplicationQuit() {
        // Reset min window grösse, sonst error
        GlobalHive.Windows.MinimumWindowSize.Reset();
    }
}