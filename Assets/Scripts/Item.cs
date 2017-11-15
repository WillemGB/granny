using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class Item {

    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public int itemPower;
    public int ItemSpeed;
    public ItemType itemType;
    
    public enum ItemType
    {
        Key,
        Power,
    }

    public Item(string name, int id, string desc, int power, int speed, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemIcon = Resources.Load<Texture2D>("Item Icons/");
        itemPower = power;
        ItemSpeed = speed;
        itemType = type;
            

    }
    public Item()
    {

    }
}
