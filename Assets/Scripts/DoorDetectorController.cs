using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetectorController : MonoBehaviour {

    public GameObject detector1;
    public GameObject detector2;
    public GameObject door;

    bool[] detectorArray = new bool[] { false, false};

    // Use this for initialization
    void Start () {
		
	}
	


	// Update is called once per frame
	void Update () {
		
	}

    void CheckForAllDetectors()
    {
        bool isBusy = true;
        for (int i = 0; i < detectorArray.Length; i++)
        {
            if (!detectorArray[i])
            {
                isBusy = false;
            }
        }

        //Open the door!
        if (isBusy)
        {
            door.GetComponent<DoorScript>().openDetectedDoor();
        }
    }

    public void isOnDetector(int number)
    {
        detectorArray[number] = true;
        print("dec" +number);

        CheckForAllDetectors();
    }

    public void isNotOnDetector(int number)
    {
        detectorArray[number] = false;
        print("notdec" + number);
    }
}
