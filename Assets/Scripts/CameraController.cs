using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

    public GameObject heli;

	private Vector3 offset;
    private Vector3 offsetHeli;

    private bool followPlayer = true;

	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
        {
            transform.position = player.transform.position + offset;
        }
	}
}
