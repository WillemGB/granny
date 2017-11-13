using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {

    public GameObject granny;
	public float moveSpeed = 0.5f;
	public int characterStrength = 250; 	
	public string controllerNumber;
	public int maxSpeed = 6;

	private Rigidbody rigidBody;
    private Animator grannyAnimator;

    private Vector3 moveInput;
	private Vector3 moveVelocity;

    private bool walking;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        walking = false;
        if (granny != null) { 
            grannyAnimator = granny.GetComponent<Animator>();
        }
    }

	void Update() {
		// gebruik controller nummer voor juiste input axis
		moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal" + controllerNumber), 0f, Input.GetAxisRaw ("Vertical" + controllerNumber));
		moveVelocity = moveInput * moveSpeed;

		walking = moveInput != Vector3.zero;
	}

	void FixedUpdate() {
		if (Math.Abs(rigidBody.velocity.x) < maxSpeed && Math.Abs(rigidBody.velocity.z) < maxSpeed) {
			rigidBody.AddForce (moveVelocity, ForceMode.Force);
		}

		// slerp model in de richting van beweging
		if(rigidBody.velocity != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rigidBody.velocity.normalized), 0.2f);

        Animate();
        
	}

	void OnTriggerStay (Collider other)
	{
		if (Input.GetButtonDown("Fire1" + controllerNumber) && other.tag == "Player" && other.GetComponent<Rigidbody>() != rigidBody)
		{
			Debug.Log("HIT enemy");
			other.gameObject.GetComponent<Rigidbody> ().AddForce(this.transform.forward * characterStrength, ForceMode.Acceleration);
			//other.gameObject.GetComponent<Rigidbody> ().velocity = this.transform.forward * 500;
		}

		if (Input.GetButtonDown("Fire1" + controllerNumber)) {
			Component[] comps = other.gameObject.GetComponents(typeof(InteractionInterface));
			foreach (Component com in comps) {
				var interactableScript = com as InteractionInterface;
				interactableScript.onUse ();
			}
		}
	}

    void Animate()
    {
        Debug.Log("ani:" + walking);
        if(granny != null)
        {
            grannyAnimator.SetBool("Walking", walking);
        }
    }


	void OnTriggerEnter(Collider other) {
		Component[] comps = other.gameObject.GetComponents(typeof(InteractionInterface));
		if (comps.Length > 0) {
			other.GetComponent<Renderer> ().material.shader = Shader.Find ("Outlined/SilhouettedDiffuseTexture");
		}
	}

	void OnTriggerExit(Collider other) {
		Component[] comps = other.gameObject.GetComponents(typeof(InteractionInterface));
		if (comps.Length > 0) {
			var shader = Shader.Find ("Diffuse");
			other.GetComponent<Renderer> ().material.shader = shader;
		}
	}

}
