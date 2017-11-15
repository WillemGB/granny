using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if ((other.name != "Player3") && other.tag == "Player")
            other.GetComponent<PlayerControl>().Stun();
    }
}
