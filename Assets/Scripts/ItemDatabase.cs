using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Apple",0,"een appel", 2 ,0, Item.ItemType.Power));
    }
    
}
