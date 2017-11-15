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
    public GameObject canUseAbility;

	public Inventory inventory;

    public GameObject stunPrefab;
    public float stunTime;

    public GameObject punchPrefab;

    private float _abilityCooldownTime;

    private Rigidbody rigidBody;
    private Animator grannyAnimator;

    private Vector3 moveInput;
	private Vector3 moveVelocity;

    public GameObject Mond;

    public GameObject KunstGebit;
	public GameObject DiarrheaPrefab;

    public float Bullet_Forward_Force;

    private bool walking;
    private float timeSinceLastCall;
    private bool isPushing;

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

        if (_abilityCooldownTime < 0)
        {

            canUseAbility.SetActive(true);
        }
        else
        {
            canUseAbility.SetActive(false);
        }

        if (Input.GetButtonDown("Fire2" + controllerNumber) && _abilityCooldownTime < 0)
	        PerformPlayerAbility();
        if (Input.GetButtonDown("Fire1" + controllerNumber))
        {
            print("push");
            isPushing = true;
        }

        timeSinceLastCall += Time.deltaTime;
        if (timeSinceLastCall >= 0.33)
        {
            isPushing = false;
            timeSinceLastCall = 0;   // reset timer back to 0
        }

        HandlePullBed(); //Jeroen: werkt alleen als de sphere collider uit staat

        Animate();
    }

    private void HandlePullBed()
    {
        if(Input.GetKey(KeyCode.LeftControl)) //pak het bed op als je P ingedrukt houdt
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit, 1f))
            {
                if (hit.transform.tag == "Bed")
                {
                    var fixedJoint = hit.collider.GetComponent<FixedJoint>();
                    fixedJoint.connectedBody = this.GetComponent<Rigidbody>();
                }
            }
        }
        else
        {
            //Jeroen todo: loskoppelen?
        }
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
				var itemId = interactableScript.loot();
				if ((controllerNumber == "" || controllerNumber == "2") && itemId == 1) {
					inventory.AddItem (itemId);
					Debug.Log ("key 1 opgepakt");
				} else if ((controllerNumber == "3" || controllerNumber == "4") && itemId == 2) {
					inventory.AddItem (itemId);
				}
			}
		}
	}

    void Animate()
    {
        //Debug.Log("ani:" + walking);
        if (granny != null && grannyAnimator != null)
        {
            grannyAnimator.SetBool("IsPushing",isPushing);
            grannyAnimator.SetBool("Walking", walking);
        }
    }


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Interactable") {
			foreach (Renderer objectRenderer in other.GetComponentsInChildren<Renderer>()) {
				foreach (Material mat in objectRenderer.materials) {
					mat.shader = Shader.Find ("Outlined/SilhouettedDiffuseTexture");
				}
			}

		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Interactable") {
			foreach (Renderer objectRenderer in other.GetComponentsInChildren<Renderer>()) {
				foreach (Material mat in objectRenderer.materials) {
					mat.shader = Shader.Find ("Diffuse");
				}
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
				Debug.Log ("Player 2 shits himself");
				Defecate ();
                break;
            case "3":
                Debug.Log("Player 3 punches");
                Punch();
                break;
            case "4":
                Debug.Log("Player 4 performs dash");
                rigidBody.AddForce(moveVelocity * 75, ForceMode.Force);
                break;
            default:
                Debug.Log("Default");
                break;
        }

        _abilityCooldownTime = abilityCooldownTime;
    }

	private void Defecate() {
		var spawnPos = this.transform.position - (transform.forward / 2);  // spawn behind player
		spawnPos.y = spawnPos.y - 1.2f;
		var randomRotation = Quaternion.Euler (0, UnityEngine.Random.Range (0, 360), 0);
		Instantiate (DiarrheaPrefab, spawnPos, randomRotation);
	}

    public void Stun()
    {
        var stun = Instantiate(stunPrefab, gameObject.transform);

        StartCoroutine(this.WaitForStunCountdown());
        walking = false;
        Animate();
        enabled = false;
        Destroy(stun, stunTime);
    }

    private void ResumeFromStun()
    {
        this.enabled = true;
    }

    private IEnumerator WaitForStunCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(stunTime);
            StopAllCoroutines();
            ResumeFromStun();
        }
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

        Destroy(Temporary_Bullet_Handler, 2.0f);
    }

    void Punch()
    {
        var punchHandler = Instantiate(punchPrefab, gameObject.transform);

        Destroy(punchHandler, 0.5f);
    }
}
