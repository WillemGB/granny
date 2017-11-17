using UnityEngine;
using UnityEngine.UI;

public class StartMenuCanvasScript : MonoBehaviour {
    public Image PressAKey;
    public RawImage BackgroundImage;
    public RawImage UpgradeImage;

    public bool readyToLoadNextScene = false;

    private Vector3 maxVector3 = new Vector3(1.01f, 1.1f, 1.1f);
    private Vector3 minVector3 = new Vector3(0.9f, 0.9f, 0.9f);

    private float tranformScale = 1.01f;

    void Awake()
    {
		UpgradeImage.enabled = false;
    }

    void FixedUpdate()
    {
        if (BackgroundImage.enabled && PressAKey.enabled)
        {
            if (PressAKey.transform.localScale.x >= maxVector3.x)
                tranformScale = 0.994f;

            if (PressAKey.transform.localScale.x <= minVector3.x)
                tranformScale = 1.005f;

            PressAKey.transform.localScale = PressAKey.transform.localScale * tranformScale;
        }
    }

    public void activateStartScreen()
    {
		BackgroundImage.enabled = false;
		UpgradeImage.enabled = true;
        PressAKey.enabled = false;
        readyToLoadNextScene = true;
    }
}
