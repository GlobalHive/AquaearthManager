using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;

public class SoldItem : Singleton<SoldItem>
{
    [FoldoutGroup("References"), Required, SceneObjectsOnly, SerializeField]
    TMP_Text id, amount, date, price;
    [FoldoutGroup("References"), Required, SceneObjectsOnly, SerializeField]
    Animator animator;

    public async void ShowSoldItem(int saleID) {
        LoadingScreen.Instance.ShowLoadingScreen("Lade Verkauf");
        var soldItemProgress = new Progress<string>();
        soldItemProgress.ProgressChanged += SoldItemProgress_ProgressChanged;

        Sales tempSoldItem = await Task.Run(() => Manager.Instance.GetSoldItemAsync(saleID));

        id.SetText($"ID: {tempSoldItem.SaleID}");
        amount.SetText($"Menge: {tempSoldItem.SoldItemsList.Count} Artikel");
        string _date = tempSoldItem.SoldDateTime.ToString("dd/MM/yyyy HH:mm");
        date.SetText($"Datum: {_date}");
        price.SetText($"CHF {tempSoldItem.FullPrice.ToString("N2")}");

        ObjectPooler.Instance.HidePooledObjects("SoldItemElement");
        foreach (Sales.SoldItems sitem in tempSoldItem.SoldItemsList) {
            Item tempItem = await Task.Run(() => Manager.Instance.GetItemAsync(sitem.ItemID, soldItemProgress));

            GameObject tempObject = ObjectPooler.Instance.GetPooledObject("SoldItemElement");
            RawImage tempRawImage = tempObject.transform.Find("Image").GetComponent<RawImage>();
            TMP_Text tempTitle = tempObject.transform.Find("Name").GetComponent<TMP_Text>();
            TMP_Text tempDescription = tempObject.transform.Find("Description").GetComponent<TMP_Text>();
            Button tempButton = tempObject.transform.Find("btnEdit").GetComponent<Button>();

            Texture2D tempImage = tempItem.Image.ToTexture();

            if (tempImage != null)
                tempRawImage.texture = tempImage;

            tempTitle.SetText(tempItem.Name);
            tempDescription.SetText($"{tempItem.Category.Name} – {sitem.Amount}x – CHF {tempItem.Price.ToString("N2")} – <u>CHF {(tempItem.Price * sitem.Amount).ToString("N2")}</u>");
            //tempButton.onClick.AddListener(() => ItemEditor.Instance.OpenItemEditor(tempItem.ID));

            tempObject.SetActive(true);
        }

        soldItemProgress.ProgressChanged -= SoldItemProgress_ProgressChanged;
        soldItemProgress = null;
        Manager.Instance.Tabs.HidePanels();
        animator.Play("Panel Open");
        LoadingScreen.Instance.HideLoadingScreen();
    }

    private void SoldItemProgress_ProgressChanged(object sender, string e) {
        LoadingScreen.Instance.SetText($"Lade Verkauf\n{e}");
    }

    public void CloseSoldItem() {
        Manager.Instance.Tabs.ShowPanels();
        ObjectPooler.Instance.HidePooledObjects("SoldItemElement");
        animator.Play("Panel Close");
    }
}
