using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{

    public StartMenuCanvasScript StartMenuCanvas;

	// Use this for initialization
	void Start () {
	    StartMenuCanvas = FindObjectOfType<StartMenuCanvasScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.anyKeyDown)
	    {
	        if (StartMenuCanvas.readyToLoadNextScene)
	            StartGame();

	        StartMenuCanvas.activateStartScreen();
	    }
	}

    void StartGame()
    {
        Debug.Log("start game load scene");
        Application.LoadLevel(1);
    }
}
