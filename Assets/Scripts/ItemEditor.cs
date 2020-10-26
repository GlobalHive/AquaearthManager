using GlobalHive.UI.ModernUI;
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

    public bool IsOpen;

    public void OpenItemEditor(Item item, EditMode editMode) {
        Manager.Instance.Tabs.HidePanels();
        switch (editMode) {
            case EditMode.Edit:
                _TitleText.SetText("Bearbeiten");
                _Name.SetTextWithoutNotify(item.Name);
                _Amount.SetTextWithoutNotify(item.Amount.ToString());
                _Price.SetTextWithoutNotify(item.Price.ToString("N2"));
                if(item.ItemImage != null)
                    _Image.texture = item.ItemImage;

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
        IsOpen = true;
    }

    public void SaveEdit() {
        
        
    }

    public void CancelEdit() {
        _ItemEditor.GetComponent<Animator>().Play("Panel Close");
        _Name.SetTextWithoutNotify(string.Empty);
        _Amount.SetTextWithoutNotify(string.Empty);
        _Price.SetTextWithoutNotify(string.Empty);
        _Image.texture = placeHolderTexture;
        IsOpen = false;
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
