﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koelkast : MonoBehaviour, InteractionInterface {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onUse(GameObject usedBy) {
		Debug.Log ("using koelkast");
	}

	public int loot() {
		return 2;
	}

	public void removeLoot() { }
}
