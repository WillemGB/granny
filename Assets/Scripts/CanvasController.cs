using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public CanvasController instance = null;
    public Text TimerText;

    // Countdown variables;
    public float MaxTimerSeconds = 180;
    private float _currentSeconds = 0;
    private bool _timerActive;

    private float _countdownSeconds = 3;
    private bool _countdownActive;
    public Image CountdownImage;

    // Countdown sprites
    public Sprite ThreeSprite;
    public Sprite TwoSprite;
    public Sprite OneSprite;
    public Sprite StartSprite;

    void Awake()
    {
        // Force unity singleton pattern for gamemanager
        if (instance == null)
            instance = this;
        // If instance already exists and it's not equal to this, then destroy this (gameObject is this). Forces singleton pattern
        else if (instance != this)
            Destroy(gameObject);

        TimerText = TimerText.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (_timerActive)
	    {
	        _currentSeconds += Time.deltaTime;

	        TimerText.text = Math.Floor(_currentSeconds / 60).ToString("00") + ":" + (_currentSeconds % 60).ToString("00");

	        if (_currentSeconds >= MaxTimerSeconds)
	        {
	            ShowEndGame();
	        }
	    }

	    if (_countdownActive)
	    {
	        _countdownSeconds -= Time.deltaTime;

	        SetCountdownImage();

            // Increase the scale of the image for a cool effect
	        CountdownImage.transform.localScale = CountdownImage.transform.localScale * 1.01f;
	    }
	}

    void StartTimer()
    {
        _timerActive = true;
    }

    void PauseTimer()
    {
        _timerActive = false;
    }

    public void ResumePlay()
    {
        CountdownImage.enabled = false;
        CountdownImage.sprite = ThreeSprite;

        _countdownActive = false;

        StartTimer();
    }

    public void StartCountdown(float countDownTimerLength)
    {
        _countdownSeconds = countDownTimerLength;

        CountdownImage.enabled = true;

        _countdownActive = true;
    }

    private void ShowEndGame()
    {
        _timerActive = false;

        TimerText.text = Math.Floor(MaxTimerSeconds / 60).ToString("00") + ":" + (MaxTimerSeconds % 60).ToString("00");
    }

    // Checks time to show the corresponding sprite
    private void SetCountdownImage()
    {
        if (_countdownSeconds < 1.0f)
        {
            if (CountdownImage.sprite != StartSprite)
            {
                CountdownImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            CountdownImage.sprite = StartSprite;
            CountdownImage.transform.localScale *= 1.8f;
            _countdownActive = false;
        }
        else if (_countdownSeconds < 2.0f)
        {
            if (CountdownImage.sprite != OneSprite)
            {
                CountdownImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            CountdownImage.sprite = OneSprite;
        }
        else if (_countdownSeconds < 3.0f)
        {
            if (CountdownImage.sprite != TwoSprite)
            {
                CountdownImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            CountdownImage.sprite = TwoSprite;
        }
        else
        {
            CountdownImage.sprite = ThreeSprite;
        }
    }
}
