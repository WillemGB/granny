using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetectorPlane : MonoBehaviour
{
    public int teamDoorNumber;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player1" || collider.name == "Player2") {
            var team = collider.GetComponent<PlayerControl>().team;

            if (team == teamDoorNumber)
                GetComponent<Collider>().isTrigger = true;
            else
                GetComponent<Collider>().isTrigger = false;
        }
        else if (collider.name == "Player3" || collider.name == "Player4") {
            var team = collider.GetComponent<PlayerControl>().team;

            if (team == teamDoorNumber)
                GetComponent<Collider>().isTrigger = true;
            else
                GetComponent<Collider>().isTrigger = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if ((collider.name == "Player1" || collider.name == "Player2"))
        {
            var team = collider.GetComponent<PlayerControl>().team;

        }
    }
}
