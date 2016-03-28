using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	public float lookSensitivity = 1;

	private float yRotation;
	private float xRotation;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (42.3506f, 88.83179f, 359.9794f);
	}
	
	// Update is called once per frame
	void Update () {
		
		xRotation += Input.GetAxis ("Mouse Y") * lookSensitivity;
		yRotation += Input.GetAxis ("Mouse X") * lookSensitivity;

		xRotation = Mathf.Clamp (xRotation, -90, 90);
		yRotation = Mathf.Clamp (yRotation, -90, 90);

		transform.rotation = Quaternion.Euler (xRotation, yRotation, 0);
	}
}
