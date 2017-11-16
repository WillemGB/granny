using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractionInterface {
	void onUseStart (GameObject usedBy);
    void onUseStop(GameObject usedBy);
    int loot ();
	void removeLoot();
}
