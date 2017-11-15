using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHideScript : MonoBehaviour {

	public LootableItem[] storageUnits;

	// Use this for initialization
	void Start () {
		foreach (LootableItem item in storageUnits) {
			item.lootableItem = -1;
		}

		int randomNumber = Random.Range (0, storageUnits.Length);
		storageUnits [randomNumber].lootableItem = 1;
		Debug.Log ("key team 1 in " + randomNumber);

		while (true) {
			int randomNumber2 = Random.Range (0, storageUnits.Length);
			if (randomNumber2 != randomNumber) {
				storageUnits [randomNumber2].lootableItem = 2;
				Debug.Log ("key team 2 in " + randomNumber2);
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
