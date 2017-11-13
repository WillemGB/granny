using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float moveSpeed = 0.5f;
	public int characterStrength = 250; 	
	public string controllerNumber;
	public int maxSpeed = 6;

	private Rigidbody rigidBody;
	private Vector3 moveInput;
	private Vector3 moveVelocity;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void Update() {
		// gebruik controller nummer voor juiste input axis
		moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal" + controllerNumber), 0f, Input.GetAxisRaw ("Vertical" + controllerNumber));
		moveVelocity = moveInput * moveSpeed;
	}

	void FixedUpdate() {
		if (rigidBody.velocity.x < maxSpeed && rigidBody.velocity.z < maxSpeed) {
			rigidBody.AddForce (moveVelocity, ForceMode.Force);
		}

		// slerp model in de richting van beweging
		if(moveInput != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput.normalized), 0.2f);

		if (Input.GetButtonDown("Fire1" + controllerNumber))
		{
			//Debug.Log("HIT");
			//other.gameObject.GetComponent<Rigidbody> ().AddForce(this.transform.forward * 5000, ForceMode.Force);
			//other.gameObject.GetComponent<Rigidbody> ().velocity = this.transform.forward * 500;
			//this.rigidBody.AddForce(this.transform.forward * 100, ForceMode.Acceleration);
		}
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
}
