using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;

	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
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

    void onPauseAnimation()
    {
        anim.enabled = false;
    }
}
