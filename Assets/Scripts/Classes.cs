using System;
using System.Collections.Generic;
using UnityEngine;

public class Category {
    public int ID;
    public string Name;
    public byte[] Image;
}

public class Item {
    public int ID, Amount;
    public string Name;
    public double Price;
    public byte[] Image;
    public Category Category;
}

public class Sales {
    public int SaleID;
    public List<SoldItems> SoldItemsList = new List<SoldItems>();
    public DateTime SoldDateTime;
    public double FullPrice;

    public class SoldItems {
        public int ItemID;
        public int Amount;
    }

}
public class DatabaseSales {
    public int ID;
    public int SalesID;
    public int ItemID;
    public int Amount;
    public DateTime DateTime;
}

public static class Extensions {
    public static Texture2D ToTexture(this byte[] bytes) {
        Texture2D tempImage = null;
        if (bytes != null) {
            tempImage = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            tempImage.LoadImage(bytes);
            tempImage.Apply();
        }
        return tempImage;
    }
}