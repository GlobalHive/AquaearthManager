using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeller : Singleton<ItemSeller>
{

    public void OpenItemSeller(List<Item> sellingItems) {
        foreach (Item item in sellingItems) {
            Debug.Log(item.Name);
        }
    }
}
