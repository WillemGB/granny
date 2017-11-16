using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour, InteractionInterface {
    private string controllernumber;
    public int loot()
    {
        return 0;
    }

    public void onUse(GameObject usedBy)
    {
        Debug.Log("Bed used by " + usedBy.tag);

        if(usedBy.tag == "Player")
        {
            var playerControl = usedBy.GetComponent<PlayerControl>();
            var fixedJoint = this.GetComponent<FixedJoint>();

            if(fixedJoint.connectedBody == null)
            {
                Debug.Log("Attach bed to player");
                fixedJoint.connectedBody = usedBy.GetComponent<Rigidbody>();
                controllernumber = playerControl.controllerNumber;
            }
            else
            {
                Debug.Log("Bed is al gekoppeld");
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
                Debug.Log("Detach bed from player");
                fixedJoint.connectedBody = null;
            }
        }
    }

    public void removeLoot()
    {
    }
}
