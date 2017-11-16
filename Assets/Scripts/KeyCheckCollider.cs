using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheckCollider : MonoBehaviour {

	bool allowTeam1 = false;
	bool allowTeam2 = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			var playerControlScript = other.gameObject.GetComponent<PlayerControl> ();

			var thisIsTeam1 = playerControlScript.controllerNumber == "" || playerControlScript.controllerNumber == "2";
			// check of we de deur kunnen unlocken voor een team
			var unlockItemId = thisIsTeam1 ? 1 : 2; 
			var inventoryItems = playerControlScript.inventory.inventory;
			foreach (Item item in inventoryItems) {
				if (item.itemID == unlockItemId) {
					Debug.Log ("key gevonden");
					if (thisIsTeam1) {
						allowTeam1 = true;
					} else {
						allowTeam2 = true;
					}
				}
			}

			if ((allowTeam1 && thisIsTeam1) || (allowTeam2 && !thisIsTeam1)) {
				Debug.Log ("allow this player through");
				var ourCollider = GetComponent<Collider> ();
				//var playerCollider = other.GetComponent<Collider> ();
				Physics.IgnoreCollision(ourCollider, other);
			}
		}
	}

}
