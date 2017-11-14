using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        anim.SetBool("OpenDoor", true);
    }

    void OnTriggerExit(Collider other)
    {
        anim.enabled = true;
        anim.SetBool("OpenDoor", false);
    }

    public void openDetectedDoor()
    {
        anim.SetBool("OpenDoorDetector", true);
    }

    void onPauseAnimation()
    {
        anim.enabled = false;
    }
}
