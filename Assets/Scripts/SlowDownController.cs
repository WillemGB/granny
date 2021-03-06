﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownController : MonoBehaviour {
    public int newMaxSpeed;
    public int origionalMaxSpeed;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<PlayerControl>().maxSpeed = newMaxSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<PlayerControl>().maxSpeed = origionalMaxSpeed;
        }
    }
}
