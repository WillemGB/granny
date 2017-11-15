using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunstgebitScript : MonoBehaviour {
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.other.name != "Player1") && collision.other.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.other.GetComponent<PlayerControl>().Stun();
        }
    }
}
