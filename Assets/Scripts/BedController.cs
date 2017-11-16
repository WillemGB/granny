using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour, InteractionInterface {
    public int loot()
    {
        return 0;
    }

    public void onUseStart(GameObject usedBy)
    {
        Debug.Log("Bed used by " + usedBy.tag);

        if(usedBy.tag == "Player")
        {
            var fixedJoint = this.GetComponent<FixedJoint>();

            Debug.Log("Attach bed to player");
            fixedJoint.connectedBody = usedBy.GetComponent<Rigidbody>();
        }
    }

    public void onUseStop(GameObject usedBy)
    {
        Debug.Log("on use stop bed");
        if (usedBy.tag == "Player")
        {
            var fixedJoint = this.GetComponent<FixedJoint>();

            Debug.Log("Detach bed from player");
            fixedJoint.connectedBody = null;
        }
    }

    public void removeLoot()
    {
    }
}
