using UnityEngine;
using UnityEngine.UI;

public class StartMenuCanvasScript : MonoBehaviour {
    public Image BackgroundImage;
    public Button StartButton;
    public Button SettingsButton;
    public Button BackButton;

	// Use this for initialization
	void Start () {
        StartButton = StartButton.GetComponent<Button>();    	
        SettingsButton = SettingsButton.GetComponent<Button>();    	
        BackButton = BackButton.GetComponent<Button>();

        // Only show back button in store
        BackButton.enabled = false;
	}
	
    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    public void ShowSettingsMenu()
    {

    }

    public void ShowStartMenu()
    {
        
    }
}
