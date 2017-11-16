using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeliController : MonoBehaviour {
    public GameObject heli;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void followHeli()
    {
        Debug.Log("cameraFollowPlayer");
        GetComponent<Animator>().SetBool("FollowHeli", true);

    }
}
