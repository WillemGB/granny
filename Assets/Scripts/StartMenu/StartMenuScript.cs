using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.anyKeyDown)
	        StartGame();
	}

    void StartGame()
    {
        Debug.Log("start game load scene");
        Application.LoadLevel(1);
    }
}
