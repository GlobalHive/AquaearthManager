using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : Singleton<Manager>
{
    [SerializeField] GameObject _CategoryTemplate, _ItemTemplate;
    [SerializeField] Transform _CategoryArray, _ItemArray;
    public GlobalHive.UI.ModernUI.ModalWindowTabs Tabs;
    private int _OpenCategory = 0;

    [SerializeField]Dictionary<int, Category> _Categorys = new Dictionary<int, Category>();
    [SerializeField]Dictionary<int, Item> _CategoryItems = new Dictionary<int, Item>();

    private void Start() {

#if UNITY_STANDALONE_WIN  // Setzt die mindest fenster grösse fest (Windows Only)
        GlobalHive.Windows.MinimumWindowSize.Set(964, 592);
#endif

        // Datenbank verbindungen starten
        GlobalHive.DatabaseAPI.API.GetInstance();

         // Startet das laden der Kategorien
        StartCoroutine(LoadCategories());
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
    }

    IEnumerator LoadCategories() {
        int currentIndex = 1;

        ClearCategories();

        // Datenbank setup
        MySqlConnection _conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand _cmd = new MySqlCommand("SELECT * FROM categorys", _conn);

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
                _Categorys.Add(currentIndex, _Cat);
                currentIndex++;

                _Cat.GetCategroyObject().SetActive(true);
                yield return null;
            }
        }
        // Schliesst und beendet die datenbank verbindung
        _cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(_conn);
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
    }

    IEnumerator LoadItems(int category) {
        int currentIndex = 1;
        _OpenCategory = category;

        ClearInventory();

        MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd;

        if (category != 0)
            cmd = new MySqlCommand($"SELECT * FROM items WHERE category = '{category}'", conn);
        else
            cmd = new MySqlCommand($"SELECT * FROM items", conn);

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

                _CategoryItems.Add(currentIndex, item);

                currentIndex++;

                item.GetItemObject().SetActive(true);
                yield return null;
            }
        }

        cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(conn);
    }

    private void ClearInventory() {
        foreach (Item item in _CategoryItems.Values) {
            Destroy(item.GetItemObject());
        }
        _CategoryItems.Clear();
    }
    #endregion

    public void OnApplicationQuit() {
        // Reset min window grösse, sonst error
        GlobalHive.Windows.MinimumWindowSize.Reset();
    }
}

[System.Serializable]
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

[System.Serializable]
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
            _Category = Manager.Instance.GetCategory(value+1);
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