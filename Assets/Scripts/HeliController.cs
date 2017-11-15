using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour {


    public GameObject grannyHeli;
    public GameObject cam1;
    public GameObject parent;
   
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.other.tag == "Player")
        {
            Debug.Log("player in Heli");

           
            collision.other.gameObject.active = false;
            grannyHeli.active = true;

            Animator animatorParent = parent.GetComponent<Animator>();
            animatorParent.SetBool("startHeli", true);

            cam1.GetComponent<CameraController>().followHeli();
            

        }
    }
}
