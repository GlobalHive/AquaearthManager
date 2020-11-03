using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class SoldItem : Singleton<SoldItem>
{
    [FoldoutGroup("References"), Required, SceneObjectsOnly, SerializeField]
    TMP_Text id, amount, date, price;
    [FoldoutGroup("References"), Required, SceneObjectsOnly, SerializeField]
    Animator animator;

    public void ShowSoldItem(Sales sale) {
        id.SetText($"ID: {sale.SaleID}");
        amount.SetText($"Menge: {sale.SoldItemsList.Count} Artikel");
        string _date = sale.SoldDateTime.ToString("dd/MM/yyyy HH:mm");
        date.SetText($"Datum: {_date}");
        price.SetText($"CHF {sale.FullPrice.ToString("N2")}");

        Manager.Instance.Tabs.HidePanels();
        animator.Play("Panel Open");
    }

    public void CloseSoldItem() {
        Manager.Instance.Tabs.ShowPanels();
        animator.Play("Panel Close");
    }
}
