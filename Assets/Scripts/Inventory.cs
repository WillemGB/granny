using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int slotsX, slotsY;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    private bool showInventory = true;
    private ItemDatabase database;

	public int playerNumber;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        

        
	}
    void Update()
    {
        //if (Input.GetButtonDown("Inventory"))
        //{
            //showInventory = !showInventory;
        //}
    }

	void OnGUI()
    {
        if (skin != null)
        {
            GUI.skin = skin;
            if (showInventory)
            {
                DrawInventory();
            }
            for (int i = 0; i < inventory.Count; i++)
            {
            //    GUI.Label(new Rect(10, i * 20, 200, 50), inventory[i].itemName);
            }
        }
	}
    void DrawInventory()
    {
        if (skin != null)
        {
            int i = 0;
            for (int x = 0; x < slotsX; x++)
            {
                for (int y = 0; y < slotsY; y++)
                {
					Rect slotRect = new Rect (x * 60, y * 60, 50, 50);
					switch (playerNumber) {
					case 1:
						slotRect = new Rect (x * 60, y * 60, 50, 50);
						break;
					case 2:
						slotRect = new Rect (Screen.width - 60, 0, 50, 50);
						break;
					case 3:
						slotRect = new Rect (0, Screen.height - 60, 50, 50);
						break;
					case 4:
						slotRect = new Rect (Screen.width - 60, Screen.height - 60, 50, 50);
						break;
					}
					//Rect slotRect = new Rect(x * 60 + inventoryXoffset, y * 60 + inventoryYoffset, 50, 50);
                    GUI.Box(slotRect, "", skin.GetStyle("Slot"));
                    slots[i] = inventory[i];
                    if (slots[i].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[i].itemIcon);
                    }


                    i++;
                }
            }
        }
    }

        public void AddItem(int id)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemName == null)
                {
                    for (int j = 0; j < database.items.Count; j++)
                    {
                        if (database.items[j].itemID == id)
                        {
                            inventory[i] = database.items[j];
                        }
                    }
                    break;
                }
            }
        }

   // bool InventoryContains(int id)
     //   for (int ;i)
   // {

   // }
    
}
