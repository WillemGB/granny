using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour {


    public GameObject grannyHeli;
    public GameObject heliCam;
    public GameObject parent;
    public GameObject gameManager;
   
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
            if (parent.transform.position.y > 6.8f && parent.transform.position.y < 7.38f)
                Time.timeScale = 0.2F;
            else
                Time.timeScale = 1.0F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
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

            gameManager.GetComponent<GameManagerControl>().startEndSound();
            gameManager.GetComponent<GameManagerControl>().setHeliCam();

            heliCam.GetComponent<CameraHeliController>().followHeli();

            //start epic end sound



        }
    }
}
