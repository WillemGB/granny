using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBookCase : MonoBehaviour, InteractionInterface
{

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onUse()
    {
        animator.SetBool("Fall", true);
        Debug.Log("pushing bookcase");
    }

	public int loot() {
		return 0;
	}

	public void removeLoot () { }

}
