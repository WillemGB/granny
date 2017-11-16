using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableItem : MonoBehaviour, InteractionInterface {

	public int lootableItem = -1;

	public void onUse (GameObject usedBy) {

	}

	public int loot () {
		return lootableItem;
	}

	public void removeLoot() {
		lootableItem = -1;
	}
}
