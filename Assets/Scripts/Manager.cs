using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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

    // Category
    [TabGroup("Category"), SerializeField, SceneObjectsOnly]
    GameObject _CategoryTemplate;
    [TabGroup("Category"), SerializeField, SceneObjectsOnly]
    Transform _CategoryArray;

    // Item
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    Button _SellButton;
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    GameObject _ItemTemplate;
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    Transform _ItemArray;
    [TabGroup("Item"), SerializeField, SceneObjectsOnly]
    TMP_Text _PaginationText;

    [FoldoutGroup("Main Canvas References"), SceneObjectsOnly]
    public GlobalHive.UI.ModernUI.ModalWindowTabs Tabs;

    int _OpenCategory = 0;
    Dictionary<int, Category> _Categorys = new Dictionary<int, Category>();
    Dictionary<int, Item> _CategoryItems = new Dictionary<int, Item>();

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
        StartCoroutine(LoadCategories());
        
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

    public Category GetCategory(int id) {
        return _Categorys[id];
    }

    public int GetCategoryCount {
        get { return _Categorys.Count; }
    }

    #region Category
    public void ReloadCategorys() {
        StartCoroutine(LoadCategories());

        selectedItems.Clear();
    }

    IEnumerator LoadCategories() {
        ClearCategories();

        // Datenbank setup
        MySqlConnection _conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand _cmd = new MySqlCommand("SELECT * FROM categorys ORDER BY name ASC", _conn);

        LoadingScreen.SetActive(true);

         // Geht alle resultate durch
        using (MySqlDataReader reader = _cmd.ExecuteReader()) {
            while (reader.Read()) {

                // Erstellt und konvertiert das bild aus der datenbank
                Texture2D image = null;
                if (!string.IsNullOrEmpty(reader["img"].ToString())) {
                    byte[] imageBytes = (byte[])reader["img"];
                    if (imageBytes.Length != 0) {
                        image = new Texture2D(256, 256, TextureFormat.RGBA32, false);
                        image.LoadImage(imageBytes);
                        image.Apply();
                    }
                }

                // Erstellt eine kategorie inklusive angezeigtes element
                Category _Cat = new Category(_CategoryTemplate, _CategoryArray, reader.GetString("name"), reader.GetInt32("id"), image);

                // Fügt den eintrag in die liste ein
                _Categorys.Add(reader.GetInt32("id"), _Cat);

                _Cat.GetCategroyObject().SetActive(true);
                yield return null;
            }
        }
        // Schliesst und beendet die datenbank verbindung
        _cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(_conn);
        LoadingScreen.SetActive(false);
    }
    public void ClearCategories() {
        // Löscht alle kategorie objekte ausser das template
        foreach (Category item in _Categorys.Values) {
            Destroy(item.GetCategroyObject());
        }
        _Categorys.Clear();
    }

    #endregion

    #region Inventory
    public void ReloadInventory(int category) {
        if(category == -1)
            StartCoroutine(LoadItems(_OpenCategory));
        else
            StartCoroutine(LoadItems(category));

        selectedItems.Clear();
        _SellButton.interactable = false;
    }

    IEnumerator LoadItems(int category) {
        _OpenCategory = category;

        ClearInventory();

        MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd;

        LoadingScreen.SetActive(true);

        if(category != 0)
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM items WHERE category = '{category}'", conn);
        else
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM items",conn);
        using (MySqlDataReader reader = cmd.ExecuteReader()) {
            reader.Read();
            MaxPage = reader.GetInt32("COUNT(*)") / 100;
        }
        cmd.Dispose();


        if (category != 0)
            cmd = new MySqlCommand($"SELECT * FROM items WHERE category = '{category}' ORDER BY name ASC LIMIT {currentPage * 100}, {currentPage+1 * 100}", conn);
        else
            cmd = new MySqlCommand($"SELECT * FROM items ORDER BY name ASC LIMIT {currentPage * 100}, {currentPage + 1 * 100}", conn);

        using (MySqlDataReader reader = cmd.ExecuteReader()) {
            while (reader.Read()) {

                Texture2D image = null;
                if (!string.IsNullOrEmpty(reader["img"].ToString())) {
                    byte[] imageBytes = (byte[])reader["img"];
                    if (imageBytes != null || imageBytes.Length != 0) {
                        image = new Texture2D(256, 256, TextureFormat.RGBA32, false);
                        image.LoadImage(imageBytes);
                        image.Apply();
                    }
                }
                
                Item item = new Item(_ItemTemplate, _ItemArray, reader.GetInt32("id"),reader.GetString("name"),reader.GetInt32("amount"), 
                    reader.GetDouble("price"), reader.GetInt32("category"), image);

                _CategoryItems.Add(reader.GetInt32("id"), item);

                item.GetItemObject().SetActive(true);
                item.GetItemObject().GetComponent<SelectableObject>().SetReturnObject(item);
                item.GetItemObject().GetComponent<SelectableObject>().OnSelectionChanged.AddListener((obj, selected) => {
                    if (selected) {
                        if(item.Amount > 0)
                            selectedItems.Add((Item)obj);
                    }
                    else {
                        selectedItems.Remove((Item)obj);
                    }

                    _SellButton.interactable = selectedItems.Count > 0;
                });

                yield return null;
            }
        }

        cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(conn);
        LoadingScreen.SetActive(false);
    }

    private void ClearInventory() {
        foreach (Item item in _CategoryItems.Values) {
            item.GetItemObject().GetComponent<SelectableObject>().OnSelectionChanged.RemoveAllListeners();
            Destroy(item.GetItemObject());
        }
        _CategoryItems.Clear();
    }
    #endregion

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

        ReloadInventory(-1);
    }

    public void OnApplicationQuit() {
        // Reset min window grösse, sonst error
        GlobalHive.Windows.MinimumWindowSize.Reset();
    }
}

public class Category {
    private GameObject _CategoryObject;
    private TMP_Text _Title;
    private RawImage _Image;

    private int _CategoryID;
    private string _CategoryName;
    private Texture _CategoryImage;

    public Texture CategoryImage {
        get { return _CategoryImage; }
        set {
            CheckReferences();
            _CategoryImage = value;
            _Image.texture = _CategoryImage;
        }
    }
    public string CategoryName {
        get { return _CategoryName; }
        set {
            _CategoryName = value;
            CheckReferences();
            _Title.SetText(_CategoryName);
        }
    }
    public int CategoryID {
        get { return _CategoryID; }
        set { _CategoryID = value; }
    }

    public Category(GameObject categoryObject, Transform parent, string name, int categoryID, Texture2D image) {

        _CategoryObject = GameObject.Instantiate(categoryObject, parent);

        CheckReferences();

        _CategoryID = categoryID;
        CategoryName = name;
        if(image != null)
            _CategoryImage = image;

        _Title.SetText(CategoryName);
        if (_CategoryImage == null)
            _CategoryImage = _Image.texture;
        _Image.texture = _CategoryImage;

        _CategoryObject.GetComponent<Button>().onClick.AddListener(() => Manager.Instance.Tabs.PanelAnim(1));
        _CategoryObject.GetComponent<Button>().onClick.AddListener(() => Manager.Instance.ReloadInventory(_CategoryID));
        
    }

    public GameObject GetCategroyObject() {
        return _CategoryObject;
    }

    private void CheckReferences() {
        if (_Title == null)
            _Title = _CategoryObject.transform.Find("Background/TextArea/Title").GetComponent<TMP_Text>();

        if(_Image == null)
            _Image = _CategoryObject.transform.Find("Background/Image").GetComponent<RawImage>();

    }
}

public class Item
{
    private GameObject _ItemObject;
    private RawImage _ItemImage;
    private TMP_Text _Title, _Description;

    private int _ID, _Amount;
    private string _Name;
    private double _Price;
    private Texture _Image;
    private Category _Category;

    public Texture ItemImage {
        get { return _Image; }
        set {
            CheckReferences();
            _Image = value;
            _ItemImage.texture = _Image;
        }
    }
    public int ID {
        get { return _ID; }
    }
    public string Name {
        get { return _Name; }
        set { _Name = value; }
    }
    public int Amount { 
        get { return _Amount; } 
        set { _Amount = value;}
    }
    public double Price {
        get { return _Price; }
        set { _Price = value; }
    }
    public int CategoryID {
        get { return _Category.CategoryID; }
        set {
            _Category = Manager.Instance.GetCategory(value);
        }
    }

    private void CheckReferences() {
        if (_ItemImage == null)
            _ItemImage = _ItemObject.transform.Find("Image").GetComponent<RawImage>();

        if (_Title == null)
            _Title = _ItemObject.transform.Find("Name").GetComponent<TMP_Text>();

        if (_Description == null)
            _Description = _ItemObject.transform.Find("Description").GetComponent<TMP_Text>();
    }

    public GameObject GetItemObject() {
        return _ItemObject;
    }

    public Item(GameObject itemObject, Transform itemParent, int id, string name, int amount, double price, int category, Texture2D image) {
        _ItemObject = GameObject.Instantiate(itemObject, itemParent);
        CheckReferences();

        _ID = id;
        _Name = name;
        _Amount = amount;
        _Price = price;
        _Category = Manager.Instance.GetCategory(category);
        _Image = image;

        _Title.SetText(_Name);
        _Description.SetText($"{_Category.CategoryName} - {_Amount}x - CHF {_Price.ToString("N2")}");
        if(image != null)
            _ItemImage.texture = _Image;

        _ItemObject.transform.Find("btnEdit").GetComponent<Button>().onClick.AddListener(() => ItemEditor.Instance.OpenItemEditor(this,ItemEditor.EditMode.Edit));
    }
}