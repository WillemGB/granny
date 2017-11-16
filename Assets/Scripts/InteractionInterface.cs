using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractionInterface {
	void onUse (GameObject usedBy);
	int loot ();
	void removeLoot();
}
