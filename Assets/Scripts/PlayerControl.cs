using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {
    public GameObject granny;
    public int team;

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

    private AudioSource audioSource;
	private AudioSource gogogoAudio;

	public AudioClip pushThatShitAudio;

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

		audioSource = GetComponents<AudioSource> () [0];
		gogogoAudio = GetComponents<AudioSource> () [1];
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

        Animate();
    }

    void OnTriggerStay (Collider other)
	{
		if (Input.GetButtonDown("Fire1" + controllerNumber) && other.tag == "Player" && other.GetComponent<Rigidbody>() != rigidBody)
		{
			Debug.Log("HIT enemy");

			var newSource = gameObject.AddComponent<AudioSource> ();
			newSource.clip = pushThatShitAudio;
			newSource.Play ();
			other.gameObject.GetComponent<Rigidbody> ().AddForce(this.transform.forward * characterStrength, ForceMode.Acceleration);
		} else if (other.tag == "Interactable" && Input.GetButtonDown("Fire1" + controllerNumber)) {
			Component[] comps = other.gameObject.GetComponents(typeof(InteractionInterface));
			foreach (Component com in comps) {
				var interactableScript = com as InteractionInterface;
				interactableScript.onUse(this.gameObject);
				var itemId = interactableScript.loot();
				if ((controllerNumber == "" || controllerNumber == "2") && itemId == 1) {
					inventory.AddItem (itemId);
					interactableScript.removeLoot();
					gogogoAudio.Play ();
					Debug.Log ("key 1 opgepakt");
				} else if ((controllerNumber == "3" || controllerNumber == "4") && itemId == 2) {
					inventory.AddItem (itemId);
					interactableScript.removeLoot();
					gogogoAudio.Play ();
					Debug.Log ("key 2 opgepakt");
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
            case "3":
                Debug.Log("Player 1 shoots fake teeth!");
                shootFakeTeeth();
                break;
			case "4":
				Debug.Log("Player 2 performs dash");
                rigidBody.AddForce(moveVelocity * 55, ForceMode.Force);
                audioSource.Play();
                break;
            case "":
                Debug.Log("Player 3 punches");
                Punch();
                break;
            case "2":
				Debug.Log ("Player 4 shits himself");
				Defecate ();
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
