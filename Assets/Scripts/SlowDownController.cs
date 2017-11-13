using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownController : MonoBehaviour {
    public int newMaxSpeed;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            var player = other.GetComponent<PlayerControl>().maxSpeed = newMaxSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            var player = other.GetComponent<PlayerControl>().maxSpeed = 6;
        }
    }
}
