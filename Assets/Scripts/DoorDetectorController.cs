using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetectorController : MonoBehaviour {

    public GameObject detector1;
    public GameObject detector2;
    public GameObject doorTeam1;
    public GameObject doorTeam2;

    bool[] detectorArrayTeamOne = new bool[] { false, false};
    bool[] detectorArrayTeamTwo = new bool[] { false, false};



    // Use this for initialization
    void Start () {
		
	}
	


	// Update is called once per frame
	void Update () {
		
	}

    void CheckForAllDetectors(int teamNumber)
    {
        bool isBusyOne = true;
        bool isBusyTwo = true;
        for (int i = 0; i < detectorArrayTeamOne.Length; i++)
        {
            if (!detectorArrayTeamOne[i])
            {
                isBusyOne = false;
            }
        }

        foreach(bool busy in detectorArrayTeamTwo)
        {
            if (!busy)
            {
                isBusyTwo = false;
            }
        }

        //Open the door!
        if (isBusyOne)
        {
            doorTeam1.GetComponent<DoorScript>().openDetectedDoor();
        }
        if (isBusyTwo)
            doorTeam2.GetComponent<DoorScript>().openDetectedDoor();
    }

    public void isOnDetector(int number, int teamNumber)
    {
        if (teamNumber == 1)
            detectorArrayTeamOne[number] = true;
        if (teamNumber == 2)
            detectorArrayTeamTwo[number] = true;

        CheckForAllDetectors(teamNumber);
    }

    public void isNotOnDetector(int number, int teamNumber)
    {
        if (teamNumber == 1) 
            detectorArrayTeamOne[number] = false;
        if (teamNumber == 2) 
            detectorArrayTeamTwo[number] = false;
    }
}
