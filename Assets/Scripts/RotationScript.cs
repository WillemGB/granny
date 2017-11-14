using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed;

	void Update () {
	    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
