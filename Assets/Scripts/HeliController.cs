﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour {


    public GameObject grannyHeli;
    public GameObject grannyTeam2Heli;
    public GameObject heliCam;
    public GameObject parent;
    public GameObject gameManager;

	public GameObject explosions;
   
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
			if (parent.transform.position.y > 15.0f && parent.transform.position.y < 15.58f)
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

			Invoke("startExplosions", 1.5f);

            collision.other.gameObject.active = false;
            if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
            {
                grannyHeli.active = true;
            }
            else if (collision.gameObject.name == "Player3" || collision.gameObject.name == "Player4")
            {
                grannyTeam2Heli.active = true;
            }

            Animator animatorParent = parent.GetComponent<Animator>();
            animatorParent.SetBool("startHeli", true);

            gameManager.GetComponent<GameManagerControl>().startEndSound();
            gameManager.GetComponent<GameManagerControl>().setHeliCam();
            gameManager.GetComponent<GameManagerControl>().resetGame();

            heliCam.GetComponent<CameraHeliController>().followHeli();

            //start epic end sound
        }
    }

	void startExplosions() {
		explosions.SetActive (true);
	}
}
