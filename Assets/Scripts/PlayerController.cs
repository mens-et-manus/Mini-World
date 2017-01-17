using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed; // speed of player movement
	public GameObject cube;
	public GameObject selectedCube;

	private Rigidbody playerObj; // player

	void Start() {
		playerObj = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {

		// movement from Keyboard arrows and Mouse Y axis
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float moveY = Input.GetAxis("Mouse Y");

		Vector3 movement = new Vector3 (moveHorizontal, moveY/5, moveVertical);

		playerObj.AddForce (movement * speed);

	}

	void OnTriggerEnter(Collider other) {

		selectedCube = other.gameObject;
		if (selectedCube.CompareTag ("Bucket")) {
			GetComponent<Renderer>().material = selectedCube.GetComponent<Renderer>().material;
		} 
		else if (selectedCube.CompareTag ("Cube")) {
			selectedCube.GetComponent<Renderer> ().material = GetComponent<Renderer> ().material;
		} 
	}
}
