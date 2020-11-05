using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
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
    public GameObject LoadingScreen;
    [FoldoutGroup("Main Canvas References"), SceneObjectsOnly]
    public GlobalHive.UI.ModernUI.ModalWindowTabs Tabs;

    // Item
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    Button _SellButton;
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

    int _OpenCategory = 0;

    List<Item> selectedItems = new List<Item>();
    int maxPage = 0;
    int currentPage = 0;

    public int MaxPage {
        get { return maxPage; }
        set {
            maxPage = value;
            if (currentPage > maxPage)
                currentPage = 0;

            _PaginationText.SetText($"{currentPage+1} / {MaxPage+1}");
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
        //_SellButton.onClick.AddListener(() => ItemSeller.Instance.OpenItemSeller(selectedItems));
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

    #region Category
    public async void LoadCategories() {
        LoadingScreen.SetActive(true); // Ladebild sichtbar machen
        
        List<Category> tempCategories = await Task.Run(() => GetCategoriesAsync()); // Startet das asynchrone laden der kategorien

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

            tempButton.onClick.AddListener(() => CategoryButtonClick(category.ID)); // Setzt die funktion beim clicken des elements
            tempObject.SetActive(true); // Macht das template sichtbar

        }

        LoadingScreen.SetActive(false); // Ladebild unsichbar machen
    }

    public void CategoryButtonClick(int categoryID) {
        _OpenCategory = categoryID; // Setzt die aktuell geöffnete kategorie fest
        LoadInventory(categoryID);
    }

    public async Task<List<Category>> GetCategoriesAsync() {
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
            }
        }
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempCategories;
    }

    public async Task<Category> GetCategoryAsync(int id) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM categorys WHERE id = '{id}'", conn);
        DbDataReader reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        Category tempCategory = new Category {
            ID = reader.GetInt32(0),
            Image = null,
            Name = reader.GetString(1)
        };
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempCategory;
    }
    #endregion

    #region Inventory
    public async void LoadInventory(int category) {
        LoadingScreen.SetActive(true);
        if (category == -1)
            category = _OpenCategory;

        MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd;
        List<Item> tempItems = new List<Item>();

        if (category != 0)
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM items WHERE category = '{category}'", conn);
        else
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM items", conn);

        object result = await cmd.ExecuteScalarAsync();
        MaxPage = int.Parse(result.ToString()) / 100;
        cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(conn);

        if (category != 0)
            tempItems = await Task.Run(()=> GetItemsAsync(new MySqlCommand($"SELECT * FROM items WHERE category = '{category}' ORDER BY name ASC LIMIT {currentPage * 100}, {currentPage + 1 * 100}")));
        else
            tempItems = await Task.Run(()=> GetItemsAsync(new MySqlCommand($"SELECT * FROM items ORDER BY name ASC LIMIT {currentPage * 100}, {currentPage + 1 * 100}")));

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
            tempDescription.SetText($"{tempItem.Category.Name} - {tempItem.Amount}x - CHF {tempItem.Price.ToString("N2")}");
            tempButton.onClick.AddListener(() => ItemEditor.Instance.OpenItemEditor(tempItem.ID));

            tempSelectableObject.SetReturnObject(tempItem);
            tempSelectableObject.OnSelectionChanged.AddListener((o, s) => OnSelectionChanged(tempItem, s));

            selectedItems.ForEach(delegate (Item _item) {
                if (_item.Name == tempItem.Name)
                    tempSelectableObject.SetSelected(true, true);
            });

            tempObject.SetActive(true);
        }
        
        _SellButton.interactable = selectedItems.Count > 0;
        Tabs.PanelAnim(1);
        LoadingScreen.SetActive(false);
    }

    public async Task<List<Item>> GetItemsAsync(MySqlCommand cmd) {
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
            }
        }
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return tempItemList;
    }

    public async Task<Item> GetItemAsync(int itemID) {
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

    public void OnItemEditButtonClick(int itemID) { 
        
    }

    public void OnSelectionChanged(object obj, bool isSelected) {
        Item item = (Item)obj;
        if (isSelected) {
            if (item.Amount > 0)
                selectedItems.Add(item);
        }
        else {
            selectedItems.Remove(item);
        }

        _SellButton.interactable = selectedItems.Count > 0;
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

        LoadInventory(-1);
    }
    #endregion

    #region Sales
    public async void LoadSales() {
        LoadingScreen.SetActive(true);
        ClearSalesList();
        Dictionary<int, Sales> _Sales = await GetSales();
        StartCoroutine(CreateSalesItems(_Sales));    }
    async Task<Dictionary<int,Sales>> GetSales() {
        List<DatabaseSales> dbs = new List<DatabaseSales>();
        Dictionary<int, Sales> sales = new Dictionary<int, Sales>();
        MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM sales ORDER BY date ASC", conn);

        using (MySqlDataReader reader = cmd.ExecuteReader()) {
            while (await reader.ReadAsync()) {
                DatabaseSales databaseSales = new DatabaseSales {
                    ID = reader.GetInt32("id"),
                    SalesID = reader.GetInt32("sale_id"),
                    ItemID = reader.GetInt32("item_id"),
                    Amount = reader.GetInt32("amount"),
                    DateTime = reader.GetDateTime("date")
                };
                dbs.Add(databaseSales);
            }
        }
        cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(conn);

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
    IEnumerator CreateSalesItems(Dictionary<int,Sales> sales) {
        foreach (Sales item in sales.Values) {
            double currentPrice = 0;

            GameObject go = Instantiate(_SalesTemplate, _SalesArray);
            go.transform.Find("Content/Title").GetComponent<TMP_Text>().SetText(item.SoldDateTime.ToString("dd/MM/yyyy HH:mm"));
            go.transform.Find("Content/Artikel").GetComponent<TMP_Text>().SetText($"{item.SoldItemsList.Count} Artikel");

            foreach (Sales.SoldItems si in item.SoldItemsList) {
                currentPrice += GetItemPrice(si.ItemID).Result * si.Amount;
            }
            item.FullPrice = currentPrice;

            go.transform.Find("Content/Price").GetComponent<TMP_Text>().SetText($"CHF {currentPrice.ToString("N2")}");
            go.GetComponent<Button>().onClick.AddListener(delegate() {
                SoldItem.Instance.ShowSoldItem(item);
            });
            go.SetActive(true);
            yield return null;
        }
        LoadingScreen.SetActive(false);
    }

    async Task<double> GetItemPrice(int itemID) {
        MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd = new MySqlCommand($"SELECT price FROM items WHERE id = '{itemID}'", conn);
        object obj = await cmd.ExecuteScalarAsync();
        return (double)obj;
    }
    public void ClearSalesList() {
        for (int i = 1; i < _SalesArray.childCount; i++) {
            Destroy(_SalesArray.GetChild(i).gameObject);
        }
    }
    #endregion

    public void OnApplicationQuit() {
        // Reset min window grösse, sonst error
        GlobalHive.Windows.MinimumWindowSize.Reset();
    }
}