using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerControl : MonoBehaviour
{
    public static GameManagerControl instance = null;
    public CanvasController CanvasControllerScript;
    public PlayerControl Player1ControllerScript;
    public PlayerControl Player2ControllerScript;
    public PlayerControl Player3ControllerScript;
    public PlayerControl Player4ControllerScript;

    private float _countDownTime = 3.0f;

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

        // Freeze player Scripts
        DisableScripts();

        // Starts Countdown
        CanvasControllerScript.StartCountdown(_countDownTime);
        StartCoroutine(this.WaitForCountdown());
    }

    // Waits for a async countdown
    private IEnumerator WaitForCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_countDownTime);
            StopAllCoroutines();
            ResumeScripts();
        }
    }

    // Enables scripts
    private void ResumeScripts()
    {
        Player1ControllerScript.enabled = true;
        Player2ControllerScript.enabled = true;
        Player3ControllerScript.enabled = true;
        Player4ControllerScript.enabled = true;
    }

    // Disables scripts so the players are "frozen"
    private void DisableScripts()
    {
        Player1ControllerScript.enabled = false;
        Player2ControllerScript.enabled = false;
        Player3ControllerScript.enabled = false;
        Player4ControllerScript.enabled = false;
    }
}
