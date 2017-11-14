using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetector : MonoBehaviour {

    public int detectorNumber;
    public GameObject detector;
    Material m_Material;


    // Use this for initialization
    void Start () {
        m_Material = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Material.color = Color.green;
            detector.GetComponent<DoorDetectorController>().isOnDetector(detectorNumber);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Material.color = Color.yellow;
            detector.GetComponent<DoorDetectorController>().isNotOnDetector(detectorNumber);
        }
    }
}
