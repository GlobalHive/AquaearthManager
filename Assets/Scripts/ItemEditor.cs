using GlobalHive.UI.ModernUI;
using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEditor : Singleton<ItemEditor>
{
    [FoldoutGroup("References"), SerializeField, SceneObjectsOnly]
    GameObject _ItemEditor;
    [FoldoutGroup("References"), SerializeField, SceneObjectsOnly]
    RawImage _Image;
    [FoldoutGroup("References"), SerializeField, SceneObjectsOnly]
    TMP_Text _TitleText;
    [FoldoutGroup("References"), SerializeField, SceneObjectsOnly]
    Button _SaveButton;
    [FoldoutGroup("References"), SerializeField, AssetsOnly]
    [InlineEditor(InlineEditorModes.LargePreview)]
    Texture placeHolderTexture = null;

    [FoldoutGroup("Input Fields"), SerializeField, SceneObjectsOnly]
    TMP_InputField _Name, _Amount, _Price;
    [FoldoutGroup("Input Fields"), SerializeField, SceneObjectsOnly]
    CustomDropdown _DropDown;

    int editItemID = 0;

    public async void OpenItemEditor(int itemID) {
        Item editItem = null;
        editItemID = itemID;

        Manager.Instance.LoadingScreen.SetActive(true);

        _DropDown.dropdownItems.Clear();

        List<Category> tempCategories = await Task.Run(() => Manager.Instance.GetCategoriesAsync());
        foreach (Category category in tempCategories) {
            CustomDropdown.Item di = new CustomDropdown.Item {
                itemIcon = IconManager.Instance.GetIcon(IconType.Arrow),
                itemName = category.Name
            };
            di.OnItemSelection = new UnityEngine.Events.UnityEvent();
            di.OnItemSelection.AddListener(delegate { OnValueChanged(); });

            _DropDown.dropdownItems.Add(di);
        }
        _DropDown.UpdateDropdown();

        if (itemID == 0) {
            _TitleText.SetText("Erstellen");
            _DropDown.ChangeDropdownInfoSilent(0);
        }
        else {
            _TitleText.SetText("Bearbeiten");

            editItem = await Task.Run(() => Manager.Instance.GetItemAsync(itemID));

            _DropDown.ChangeDropdownInfoSilent(editItem.Category.Name);
            _Name.SetTextWithoutNotify(editItem.Name);
            _Amount.SetTextWithoutNotify(editItem.Amount.ToString());
            _Price.SetTextWithoutNotify(editItem.Price.ToString("N2"));

            Texture2D tempTexture = editItem.Image.ToTexture();
            if (tempTexture != null)
                _Image.texture = tempTexture;
        }

        Manager.Instance.Tabs.HidePanels();
        _ItemEditor.GetComponent<Animator>().Play("Panel Open");
        Manager.Instance.LoadingScreen.SetActive(false);
    }

    public void CreateItem() {
        OpenItemEditor(0);
    }

    public async void SaveEdit() {
        Item editItem = null;

        if (editItemID == 0)
            editItem = new Item();
        else
            editItem = await Task.Run(() => Manager.Instance.GetItemAsync(editItemID));

        editItem.Name = _Name.text;
        editItem.Amount = int.Parse(_Amount.text);
        editItem.Price = double.Parse(_Price.text);
        editItem.Category = await Task.Run(() => Manager.Instance.GetCategoryAsync(_DropDown.selectedItemIndex + 1));

        Texture2D image = _Image.texture as Texture2D;
        editItem.Image = image.EncodeToPNG();

        Manager.Instance.LoadingScreen.SetActive(true);
        await Task.Run(()=> SaveItem(editItem));
        CancelEdit();
        Manager.Instance.LoadInventory(-1);
    }

    async Task SaveItem(Item item) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = null;
        string updateString = "UPDATE items SET name=@NAME,img=@IMG,amount=@AMOUNT,price=@PRICE,category=@CATEGORY WHERE id = @ID";
        string insertString = "INSERT INTO items (name, img, amount, price, category) VALUES (@NAME, @IMG, @AMOUNT, @PRICE, @CATEGORY)";

        if (string.IsNullOrEmpty(_Name.text))
            _Name.SetTextWithoutNotify("ERROR #001 - NNF");
        if (string.IsNullOrEmpty(_Amount.text))
            _Amount.SetTextWithoutNotify("0");
        if (string.IsNullOrEmpty(_Price.text))
            _Price.SetTextWithoutNotify("0.00");

        if (editItemID == 0) {
            cmd = new MySqlCommand(insertString, conn);
            cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 255);
            cmd.Parameters.Add("@IMG", MySqlDbType.MediumBlob);
            cmd.Parameters.Add("@AMOUNT", MySqlDbType.Int32);
            cmd.Parameters.Add("@PRICE", MySqlDbType.Double);
            cmd.Parameters.Add("@CATEGORY", MySqlDbType.Int32);

            cmd.Parameters["@NAME"].Value = item.Name;
            cmd.Parameters["@IMG"].Value = item.Image;
            cmd.Parameters["@AMOUNT"].Value = item.Amount;
            cmd.Parameters["@PRICE"].Value = item.Price;
            cmd.Parameters["@CATEGORY"].Value = item.Category.ID;
        } else {
            cmd = new MySqlCommand(updateString, conn);
            cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 255);
            cmd.Parameters.Add("@IMG", MySqlDbType.MediumBlob);
            cmd.Parameters.Add("@AMOUNT", MySqlDbType.Int32);
            cmd.Parameters.Add("@PRICE", MySqlDbType.Double);
            cmd.Parameters.Add("@CATEGORY", MySqlDbType.Int32);
            cmd.Parameters.Add("@ID", MySqlDbType.Int32);

            cmd.Parameters["@NAME"].Value = item.Name;
            cmd.Parameters["@IMG"].Value = item.Image;
            cmd.Parameters["@AMOUNT"].Value = item.Amount;
            cmd.Parameters["@PRICE"].Value = item.Price;
            cmd.Parameters["@CATEGORY"].Value = item.Category.ID;
            cmd.Parameters["@ID"].Value = item.ID;
        }

        await cmd.ExecuteNonQueryAsync();
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
    }

    public void CancelEdit() {
        _ItemEditor.GetComponent<Animator>().Play("Panel Close");
        _Name.SetTextWithoutNotify(string.Empty);
        _Amount.SetTextWithoutNotify(string.Empty);
        _Price.SetTextWithoutNotify(string.Empty);
        _Image.texture = placeHolderTexture;
        _SaveButton.interactable = false;
        Manager.Instance.Tabs.ShowPanels();
    }

    public void SelectImage() {
        var extensions = new[] {
            new SFB.ExtensionFilter("Image Files","jpg", "png", "hvei")
        };
        string[] selectedImage = SFB.StandaloneFileBrowser.OpenFilePanel("Auswählen", "", extensions, false);
        if (selectedImage.Length > 0) {
            System.IO.FileStream fs = new System.IO.FileStream(selectedImage[0], System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            byte[] imageData = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            Texture2D image = new Texture2D(256, 256);
            image.LoadImage(imageData);
            image.Apply();
            _Image.texture = image;
            OnValueChanged();
        }
        else
            _Image.texture = placeHolderTexture;

    }

    public void OnValueChanged() {
        if(!_SaveButton.interactable)
            _SaveButton.interactable = true;
    }
}