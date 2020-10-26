using GlobalHive.UI.ModernUI;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEditor : Singleton<ItemEditor>
{
    [SerializeField] TMP_Text _TitleText;
    [SerializeField] TMP_InputField _Name, _Amount, _Price;
    [SerializeField] CustomDropdown _DropDown;
    [SerializeField] Button _SaveButton;
    [SerializeField] RawImage _Image;
    [SerializeField] GameObject _ItemEditor;

    [SerializeField]Texture placeHolderTexture = null;
    EditMode currentMode;
    Item editItem = null;

    public void OpenItemEditor(Item item, EditMode editMode) {
        Manager.Instance.Tabs.HidePanels();
        currentMode = editMode;
        editItem = item;
        switch (editMode) {
            case EditMode.Edit:
                _TitleText.SetText("Bearbeiten");
                _Name.SetTextWithoutNotify(editItem.Name);
                _Amount.SetTextWithoutNotify(editItem.Amount.ToString());
                _Price.SetTextWithoutNotify(editItem.Price.ToString("N2"));
                if(editItem.ItemImage != null)
                    _Image.texture = editItem.ItemImage;

                _DropDown.dropdownItems.Clear();
                for (int i = 0; i < Manager.Instance.GetCategoryCount; i++) {

                    Category curCat = Manager.Instance.GetCategory(i+1);

                    CustomDropdown.Item di = new CustomDropdown.Item {
                        itemIcon = IconManager.Instance.GetIcon(IconType.Arrow),
                        itemName = curCat.CategoryName
                    };
                    di.OnItemSelection = new UnityEngine.Events.UnityEvent();
                    di.OnItemSelection.AddListener(delegate { OnValueChanged(); });

                    _DropDown.dropdownItems.Add(di);
                }
                _DropDown.UpdateDropdown();
                _DropDown.ChangeDropdownInfoSilent(item.CategoryID - 1);
                break;
            case EditMode.Create:
                _TitleText.SetText("Erstellen");
                break;
            default:
                break;
        }

        _ItemEditor.GetComponent<Animator>().Play("Panel Open");
    }

    public async void SaveEdit() {
        editItem.Name = _Name.text;
        editItem.Amount = int.Parse(_Amount.text);
        editItem.Price = double.Parse(_Price.text);
        editItem.CategoryID = _DropDown.selectedItemIndex;
        editItem.ItemImage = _Image.texture;

        await SaveItem(editItem, currentMode);

        CancelEdit();
        Manager.Instance.ReloadInventory(-1);
    }

    async Task SaveItem(Item item, EditMode mode) {
        MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd = new MySqlCommand();
        string updateString = "UPDATE items SET name=@NAME,img=@IMG,amount=@AMOUNT,price=@PRICE,category=@CATEGORY WHERE id = @ID";

        Texture2D image = item.ItemImage as Texture2D;
        byte[] imageBytes = image.EncodeToPNG();

        switch (mode) {
            case EditMode.Create:
                break;
            case EditMode.Edit:
                cmd = new MySqlCommand(updateString, conn);
                cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 30);
                cmd.Parameters.Add("@IMG", MySqlDbType.MediumBlob);
                cmd.Parameters.Add("@AMOUNT", MySqlDbType.Int32);
                cmd.Parameters.Add("@PRICE", MySqlDbType.Double);
                cmd.Parameters.Add("@CATEGORY", MySqlDbType.Int32);
                cmd.Parameters.Add("@ID", MySqlDbType.Int32);

                cmd.Parameters["@NAME"].Value = item.Name;
                cmd.Parameters["@IMG"].Value = imageBytes;
                cmd.Parameters["@AMOUNT"].Value = item.Amount;
                cmd.Parameters["@PRICE"].Value = item.Price;
                cmd.Parameters["@CATEGORY"].Value = item.CategoryID;
                cmd.Parameters["@ID"].Value = item.ID;
                break;
        }
        if (cmd != null) {
            await cmd.ExecuteNonQueryAsync();
            cmd.Dispose();
            GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(conn);
        }
            
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

    public enum EditMode { 
        Create,
        Edit
    }
}
