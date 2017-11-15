using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{

    public int speed;
    public Boolean isTriggered;

    private AudioSource audioSource;

    public AudioClip epicSound;

    // Use this for initialization
    void Start ()
	{
	    isTriggered = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.other.tag == "Player")
        {
            isTriggered = true;

            Rigidbody helicopterRigidbody = GetComponent<Rigidbody>();
            helicopterRigidbody.isKinematic = true;

            Rigidbody playerRigidbody = collision.other.GetComponent<Rigidbody>();
            playerRigidbody.isKinematic = true;

            PlayerControl playertje = collision.other.GetComponent<PlayerControl>();
            // playertje.isInEndScene = true;

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = epicSound;
            audioSource.Play();
        }
    }
}
