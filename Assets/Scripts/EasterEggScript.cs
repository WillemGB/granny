using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (Collider.tag == "Player")
            Application.LoadLevel(2);
    }
}
