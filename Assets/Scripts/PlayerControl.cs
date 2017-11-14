using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {

    public GameObject granny;
	public float moveSpeed = 0.5f;
	public int characterStrength = 250; 	
	public string controllerNumber;
	public int maxSpeed = 6;
    public float abilityCooldownTime;

    private float _abilityCooldownTime;

    private Rigidbody rigidBody;
    private Animator grannyAnimator;

    private Vector3 moveInput;
	private Vector3 moveVelocity;

    public GameObject Mond;

    public GameObject KunstGebit;

    public float Bullet_Forward_Force;

    private bool walking;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        walking = false;
        if (granny != null) { 
            grannyAnimator = granny.GetComponent<Animator>();
        }
        _abilityCooldownTime = -0.1F;
    }

	void Update() {
		// gebruik controller nummer voor juiste input axis
		moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal" + controllerNumber), 0f, Input.GetAxisRaw ("Vertical" + controllerNumber));
		moveVelocity = moveInput * moveSpeed;

		walking = moveInput != Vector3.zero;

        if (_abilityCooldownTime > 0)
	        _abilityCooldownTime -= Time.deltaTime;
	}

	void FixedUpdate() {
		if (Math.Abs(rigidBody.velocity.x) < maxSpeed && Math.Abs(rigidBody.velocity.z) < maxSpeed) {
			rigidBody.AddForce (moveVelocity, ForceMode.Force);
		}

		// slerp model in de richting van beweging
		if(rigidBody.velocity != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rigidBody.velocity.normalized), 0.2f);

        Animate();

        if (Input.GetButtonDown("Fire1" + controllerNumber) && _abilityCooldownTime < 0)
	        PerformPlayerAbility();
	}

	void OnTriggerStay (Collider other)
	{
		if (Input.GetButtonDown("Fire1" + controllerNumber) && other.tag == "Player" && other.GetComponent<Rigidbody>() != rigidBody)
		{
			Debug.Log("HIT enemy");
			other.gameObject.GetComponent<Rigidbody> ().AddForce(this.transform.forward * characterStrength, ForceMode.Acceleration);
		} else if (other.tag == "Interactable" && Input.GetButtonDown("Fire1" + controllerNumber)) {
			Component[] comps = other.gameObject.GetComponents(typeof(InteractionInterface));
			foreach (Component com in comps) {
				var interactableScript = com as InteractionInterface;
				interactableScript.onUse ();
			}
		}
	}

    void Animate()
    {
        //Debug.Log("ani:" + walking);
        if(granny != null)
        {
            grannyAnimator.SetBool("Walking", walking);
        }
    }


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Interactable") {
			Component[] comps = other.gameObject.GetComponents (typeof(InteractionInterface));
			if (comps.Length > 0) {
				other.GetComponent<Renderer> ().material.shader = Shader.Find ("Outlined/SilhouettedDiffuseTexture");
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Interactable") {
			Component[] comps = other.gameObject.GetComponents (typeof(InteractionInterface));
			if (comps.Length > 0) {
				var shader = Shader.Find ("Diffuse");
				other.GetComponent<Renderer> ().material.shader = shader;
			}
		}
	}

    void PerformPlayerAbility()
    {
        // Perform action based on controller number
        switch (controllerNumber)
        {
            case "":
                Debug.Log("Player 1 shoots fake teeth!");
                shootFakeTeeth();
                break;
            case "2":
                Debug.Log("Player 2 performs ...");
                break;
            case "3":
                Debug.Log("Player 3 performs ...");
                break;
            case "4":
                Debug.Log("Player 4 performs dash");
                rigidBody.AddForce(moveVelocity * 30, ForceMode.Force);
                break;
            default:
                Debug.Log("Default");
                break;
        }

        _abilityCooldownTime = abilityCooldownTime;
    }

    void shootFakeTeeth()
    {
        //The Bullet instantiation happens here.
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(KunstGebit, Mond.transform.position, Mond.transform.rotation) as GameObject;

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(Temporary_Bullet_Handler, 10.0f);
    }
}
