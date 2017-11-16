using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetector : MonoBehaviour {

    public int detectorNumber;
    public GameObject detector;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var teamNumber = other.GetComponent<PlayerControl>().team;
            detector.GetComponent<DoorDetectorController>().isOnDetector(detectorNumber, teamNumber);

            var audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            var teamNumber = other.GetComponent<PlayerControl>().team;
            detector.GetComponent<DoorDetectorController>().isNotOnDetector(detectorNumber, teamNumber);
        }
    }
}
