using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float moveSpeed;
	public string controllerNumber;

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
		rigidBody.velocity = moveVelocity;

		// slerp model in de richting van beweging
		if(moveInput != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput.normalized), 0.2f);
	}
}
