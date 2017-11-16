using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour, InteractionInterface {
    private string controllernumber;
    public int loot()
    {
        return 0;
    }

    public void onUse(GameObject usedBy)
    {
        Debug.Log(name + " used by " + usedBy.name);

        if(usedBy.tag == "Player")
        {
            var playerControl = usedBy.GetComponent<PlayerControl>();
            var fixedJoint = this.GetComponent<FixedJoint>();

            if(fixedJoint.connectedBody == null)
            {
                Debug.Log(name + "attached to player " + usedBy.name);
                fixedJoint.connectedBody = usedBy.GetComponent<Rigidbody>();
                controllernumber = playerControl.controllerNumber;
            }
            else
            {
                Debug.Log(name + " already attached to " + fixedJoint.connectedBody.name);
            }
        }
    }

    public void Update()
    {
        if (Input.GetButtonUp("Fire1" + controllernumber)) //button released
        {
            var fixedJoint = this.GetComponent<FixedJoint>();
            if (fixedJoint != null && fixedJoint.connectedBody != null)
            {
                Debug.Log(name + " detached from " + fixedJoint.connectedBody.name);
                fixedJoint.connectedBody = null;
            }
        }
    }

    public void removeLoot()
    {
    }
}
