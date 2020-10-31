using GlobalHive.UI.ModernUI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSeller : Singleton<ItemSeller>
{
    [FoldoutGroup("References"), Required, SerializeField]
    Animator animator;
    [FoldoutGroup("References"), Required, SerializeField]
    GameObject sellingListTemplate;
    [FoldoutGroup("References"), Required, SerializeField]
    Transform sellingListList;
    [FoldoutGroup("References"), Required, SerializeField]
    TMP_Text valueText;

    List<Item> sellingItems;

    public void OpenItemSeller(List<Item> _sellingItems) {
        sellingItems = _sellingItems;

        StartCoroutine(CreateSellingList());
        animator.Play("Panel Open");
        Manager.Instance.Tabs.HidePanels();
    }

    IEnumerator CreateSellingList() {
        for (int i = 0; i < sellingItems.Count; i++) {
            CreateSellingObject(sellingItems[i]);
            yield return null;
        }
    }

    public void CreateSellingObject(Item item) {
        GameObject go = Instantiate(sellingListTemplate, sellingListList);
        RawImage rawImage = go.transform.Find("Image").GetComponent<RawImage>();
        TMP_Text name = go.transform.Find("Title").GetComponent<TMP_Text>();
        TMP_Text category = go.transform.Find("Category").GetComponent<TMP_Text>();
        CustomDropdown amount = go.transform.Find("DropDownAmount").GetComponent<CustomDropdown>();
        TMP_Text price = go.transform.Find("Price").GetComponent<TMP_Text>();

        rawImage.texture = item.ItemImage;
        name.SetText(item.Name);
        category.SetText(Manager.Instance.GetCategory(item.CategoryID).CategoryName);
        for (int i = 0; i < item.Amount+1; i++) {
            CustomDropdown.Item _item = new CustomDropdown.Item();
            _item.itemIcon = null;
            _item.itemName = i.ToString();
            _item.OnItemSelection = new UnityEngine.Events.UnityEvent();
            _item.OnItemSelection.AddListener(() => OnValueChanged(amount,item));
            amount.dropdownItems.Add(_item);
        }
        amount.ChangeDropdownInfo(1);
        //price.SetText("CHF " + (item.Price * amount.selectedItemIndex).ToString("N2"));

        go.SetActive(true);
    }

    public void OnValueChanged(CustomDropdown dp, Item value) {
        TMP_Text t = dp.transform.parent.Find("Price").GetComponent<TMP_Text>();
        t.SetText("CHF " + (value.Price * dp.selectedItemIndex).ToString("N2"));
        valueText.SetText("CHF " + RecalculatePrice().ToString("N2"));
    }

    public double RecalculatePrice() {
        double price = 0.00;
        for (int i = 0; i < sellingListList.childCount-1; i++) {
            price += sellingListList.GetChild(i+1).Find("DropDownAmount").GetComponent<CustomDropdown>().selectedItemIndex * sellingItems[i].Price;
        }
        return price;
    }
}
