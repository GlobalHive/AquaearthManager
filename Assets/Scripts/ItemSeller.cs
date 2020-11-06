using GlobalHive.UI.ModernUI;
using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        ObjectPooler.Instance.HidePooledObjects("SellingItemElement");

        foreach (int item in _sellingItems) {
            Item tempItem = await Task.Run(() => Manager.Instance.GetItemAsync(item, sellingItemProgress));
            if (tempItem.Amount == 0)
                continue;
            CreateSellingObject(tempItem);
        }

        valueText.SetText("CHF " + RecalculatePrice().ToString("N2"));

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

        amount.ClearDropDown();
        for (int i = 0; i < item.Amount+1; i++) {
            CustomDropdown.Item _item = new CustomDropdown.Item();
            _item.itemIcon = null;
            _item.itemName = i.ToString();
            _item.OnItemSelection = new UnityEngine.Events.UnityEvent();
            _item.OnItemSelection.AddListener(() => OnValueChanged(amount,item));
            amount.dropdownItems.Add(_item);
        }
        amount.UpdateDropdown();
        amount.ChangeDropdownInfo(1);
        go.SetActive(true);
    }

    public void OnValueChanged(CustomDropdown dp, Item value) {
        TMP_Text t = dp.transform.parent.Find("Price").GetComponent<TMP_Text>();
        t.SetText("CHF " + (value.Price * dp.selectedItemIndex).ToString("N2"));
        valueText.SetText("CHF " + RecalculatePrice().ToString("N2"));
    }

    public double RecalculatePrice() {
        double price = 0.00;

        for (int i = 0; i < ObjectPooler.Instance.itemsToPool[4].parent.childCount; i++) {
            GameObject tempObject = ObjectPooler.Instance.itemsToPool[4].parent.GetChild(i).gameObject;
            if (tempObject.activeInHierarchy) {
                string tempOldPrice = tempObject.transform.Find("Content/Price").GetComponent<TMP_Text>().text;
                double oldPrice = double.Parse(tempOldPrice.Remove(0, 3));
                price += oldPrice;
            }
        }
        return price;
    }

    public void CancelSellingItems() {
        ObjectPooler.Instance.HidePooledObjects("SellingItemElement");
        Manager.Instance.Tabs.ShowPanels();
        animator.Play("Panel Close");
    }

    public async void OnItemSellerSave() {
        LoadingScreen.Instance.ShowLoadingScreen("Speichere Verkauf");
        int amount = 0;
        int saleID = await Task.Run(() => GetSaleID());


        for (int i = 0; i < sellingItems.Count; i++) {
            amount = ObjectPooler.Instance.itemsToPool[4].parent.GetChild(i).Find("Content/DropDownAmount").GetComponent<CustomDropdown>().selectedItemIndex;

            if (amount == 0)
                continue;

            await Task.Run(() => SaveSalesToDatabase(saleID,sellingItems[i], amount));
            
        }

        animator.Play("Panel Close");
        sellingItems.Clear();
        Manager.Instance.selectedItems.Clear();
        ObjectPooler.Instance.HidePooledObjects("SellingItemElement");
        Manager.Instance.Tabs.ShowPanels();
        Manager.Instance.LoadInventory(-1);
    }

    public async Task SaveSalesToDatabase(int sale_id, int itemID, int amount) {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand($"INSERT INTO sales (sale_id, item_id, amount) VALUES ('{sale_id}', '{itemID}', '{amount}'); " +
            $"UPDATE items SET amount = amount{-amount} WHERE id = '{itemID}';", conn);

        await cmd.ExecuteNonQueryAsync();
        
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
    }

    public async Task<int> GetSaleID() {
        MySqlConnection conn = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = new MySqlCommand("SELECT sale_id FROM sales ORDER BY id DESC LIMIT 1", conn);
        int lastSaleID = (int)cmd.ExecuteScalarAsync().Result + 1;
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(conn);
        return lastSaleID;
    }
}
