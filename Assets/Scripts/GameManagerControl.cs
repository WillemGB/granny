using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerControl : MonoBehaviour
{
    public static GameManagerControl instance = null;
	public bool debugging = false;
    public CanvasController CanvasControllerScript;
    public PlayerControl Player1ControllerScript;
    public PlayerControl Player2ControllerScript;
    public PlayerControl Player3ControllerScript;
    public PlayerControl Player4ControllerScript;

    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject cam4;
    public GameObject camHeli;

    public AudioClip endSound;

	private AudioSource backgroundMusic;

    private float _countDownTime = 4.7f;

    void Awake()
    {
        // Force unity singleton pattern for gamemanager
        if (instance == null)
            instance = this;
        // If instance already exists and it's not equal to this, then destroy this (gameObject is this). Forces singleton pattern
        else if (instance != this)
            Destroy(gameObject);

        // Init the canvas controller
        CanvasControllerScript = FindObjectOfType<CanvasController>();
		backgroundMusic = GetComponent<AudioSource>();

        
    }

	void Start() {
		if (!debugging) {
			// Freeze player Scripts
			DisableScripts();

			// Starts Countdown
			CanvasControllerScript.StartCountdown(_countDownTime);
			StartCoroutine(this.WaitForCountdown());
		}

		backgroundMusic.Play ();
	}

    // Waits for a async countdown
    private IEnumerator WaitForCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_countDownTime);
            StopAllCoroutines();
            ResumeGame();
        }
    }

    // Enables scripts
    private void ResumeGame()
    {
        Player1ControllerScript.enabled = true;
        Player2ControllerScript.enabled = true;
        Player3ControllerScript.enabled = true;
        Player4ControllerScript.enabled = true;

        CanvasControllerScript.ResumePlay();
    }

    // Disables scripts so the players are "frozen"
    private void DisableScripts()
    {
        Player1ControllerScript.enabled = false;
        Player2ControllerScript.enabled = false;
        Player3ControllerScript.enabled = false;
        Player4ControllerScript.enabled = false;
    }

    public void startEndSound()
    {
        backgroundMusic.clip = endSound;
        backgroundMusic.Play();
    }

    public void setHeliCam()
    {
        cam1.active = false;
        cam2.active = false;
        cam3.active = false;
        cam4.active = false;
        camHeli.active = true;
    }

    public void resetGame()
    {
        Invoke("GoToStartScreen", 12f);
    }

    
    void GoToStartScreen()
    {
        Application.LoadLevel(0);
    }
}
