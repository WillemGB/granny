using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliSoundScript : MonoBehaviour {

    AudioSource sound;
    //AudioClip heliSound;
    bool musicIsPlaying = false;

    float startSound = 0.1f;

	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (musicIsPlaying)
        {
            Debug.Log("vol:" + startSound);
            if (startSound <= 1.0f)
            {
                sound.volume = startSound;
                startSound += 0.1f;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if(!musicIsPlaying && other.tag == "Player")
        {
            musicIsPlaying = true;
            sound.Play();
        }
    }
}
