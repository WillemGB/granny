using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    private bool showInventory;
    private ItemDatabase database;


    // Use this for initialization
    void Start() {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        print(inventory.Count);
        inventory.Add(database.items[0]);
        inventory.Add(database.items[1]);
        inventory.Add(database.items[0]);
        inventory.Add(database.items[1]);
        print(inventory[0].itemName);

        
	}
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

	void OnGUI()
    {
        if(showInventory)
        {
            DrawInventory();
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10,i * 20,200,50), inventory[i].itemName);
        }
	}
    void DrawInventory()
    {
        for(int x = 0; x < slotsX; x++)
        {
            for (int y = 0; y < slotsY; y++)
            {
                GUI.Box(new Rect(x * 20, y * 20, 20,20), y.ToString());
            }
        }
    }
}
