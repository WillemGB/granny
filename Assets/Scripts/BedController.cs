using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour, InteractionInterface {
    public int loot()
    {
        return 0;
    }

    public void onUse(GameObject usedBy)
    {
        var bla = usedBy.tag;
        Debug.Log("Bed used by" + usedBy.tag);
    }

    public void removeLoot()
    {
    }
}
