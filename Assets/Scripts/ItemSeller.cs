using GlobalHive.UI.ModernUI;
using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSeller : Singleton<ItemSeller>
{
    [FoldoutGroup("References"), Required, SerializeField]
    Animator animator;
    [FoldoutGroup("References"), Required, SerializeField]
    TMP_Text valueText;

    List<int> sellingItems;

    public async void OpenItemSeller(List<int> _sellingItems) {
        LoadingScreen.Instance.ShowLoadingScreen("Lade Verkauf");
        var sellingItemProgress = new Progress<string>();
        sellingItemProgress.ProgressChanged += SellingItemProgress_ProgressChanged;

        sellingItems = _sellingItems;

        foreach (int item in _sellingItems) {
            Item tempItem = await Task.Run(() => Manager.Instance.GetItemAsync(item, sellingItemProgress));
            if (tempItem.Amount == 0)
                continue;
            CreateSellingObject(tempItem);
        }
     
        animator.Play("Panel Open");
        Manager.Instance.Tabs.HidePanels();

        sellingItemProgress.ProgressChanged -= SellingItemProgress_ProgressChanged;
        sellingItemProgress = null;
        LoadingScreen.Instance.HideLoadingScreen();
    }

    private void SellingItemProgress_ProgressChanged(object sender, string e) {
        LoadingScreen.Instance.SetText($"Lade Verkauf\n{e}");
    }

    public void CreateSellingObject(Item item) {
        GameObject go = ObjectPooler.Instance.GetPooledObject("SellingItemElement");
        RawImage rawImage = go.transform.Find("Image").GetComponent<RawImage>();
        TMP_Text name = go.transform.Find("Content/Title").GetComponent<TMP_Text>();
        TMP_Text category = go.transform.Find("Content/Category").GetComponent<TMP_Text>();
        CustomDropdown amount = go.transform.Find("Content/DropDownAmount").GetComponent<CustomDropdown>();
        TMP_Text price = go.transform.Find("Content/Price").GetComponent<TMP_Text>();

        rawImage.texture = item.Image.ToTexture();
        name.SetText(item.Name);
        category.SetText(item.Category.Name);

        amount.dropdownItems.ForEach(i => i.OnItemSelection.RemoveAllListeners());
        amount.dropdownItems.Clear();
        for (int i = 0; i < item.Amount+1; i++) {
            CustomDropdown.Item _item = new CustomDropdown.Item();
            _item.itemIcon = null;
            _item.itemName = i.ToString();
            _item.OnItemSelection = new UnityEngine.Events.UnityEvent();
            _item.OnItemSelection.AddListener(() => OnValueChanged(amount,item));
            amount.dropdownItems.Add(_item);
        }
        amount.ChangeDropdownInfo(1);
        go.SetActive(true);
    }

    public void OnValueChanged(CustomDropdown dp, Item value) {
        TMP_Text t = dp.transform.parent.Find("Price").GetComponent<TMP_Text>();
        t.SetText("CHF " + (value.Price * dp.selectedItemIndex).ToString("N2"));
        //valueText.SetText("CHF " + RecalculatePrice().ToString("N2"));
    }

    public double RecalculatePrice() {
        double price = 0.00;
        /*for (int i = 0; i < sellingListList.childCount-1; i++) {
            price += sellingListList.GetChild(i+1).Find("Content/DropDownAmount").GetComponent<CustomDropdown>().selectedItemIndex * sellingItems[i].Price;
        }*/
        return price;
    }

    public void CancelSellingItems() {
        ObjectPooler.Instance.HidePooledObjects("SellingItemElement");
        Manager.Instance.Tabs.ShowPanels();
        animator.Play("Panel Close");
    }

    public async void OnItemSellerSave() {
        /*MySqlConnection conn = GlobalHive.DatabaseAPI.API.GetInstance().GetConnection();
        MySqlCommand cmd = new MySqlCommand("SELECT sale_id FROM sales ORDER BY id DESC LIMIT 1", conn);
        int lastSalesId = 0;
        int amount = 0;

        using (MySqlDataReader reader = cmd.ExecuteReader()) {
            while (reader.Read()) {
                lastSalesId = reader.GetInt32("sale_id");
                lastSalesId++;
            }
        }

        for (int i = 0; i < sellingItems.Count; i++) {
            amount = sellingListList.GetChild(i+1).Find("Content/DropDownAmount").GetComponent<CustomDropdown>().selectedItemIndex;
            if (amount == 0)
                continue;

            cmd.CommandText = $"INSERT INTO sales (sale_id, item_id, amount) VALUES ('{lastSalesId}', '{sellingItems[i].ID}', '{amount}'); " +
                $"UPDATE items SET amount = '{sellingItems[i].Amount - amount}' WHERE id = '{sellingItems[i].ID}';";
            await cmd.ExecuteNonQueryAsync();
        }

        cmd.Dispose();
        GlobalHive.DatabaseAPI.API.GetInstance().FreeConnection(conn);

        animator.Play("Panel Close");
        sellingItems.Clear();
        for (int i = 1; i < sellingListList.childCount; i++) {
            Destroy(sellingListList.GetChild(i).gameObject);
        }
        Manager.Instance.Tabs.ShowPanels();
        Manager.Instance.ReloadInventory(-1);*/
    }
}
