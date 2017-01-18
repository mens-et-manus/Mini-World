using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed; // speed of player movement
	public GameObject cube;
	public GameObject selectedCube;
	public GameObject templateShape;


	private Rigidbody playerObj; // player
	private float zPos;
	private float zOffset;

	void Start() {
		playerObj = GetComponent<Rigidbody> ();
		zOffset = 1.0f;
		zPos = 0.0f;
	}

	void FixedUpdate() {
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			zPos -= zOffset;
		} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			zPos += zOffset;
		}
		playerObj.transform.position = new Vector3 (Input.mousePosition[0]/100, Input.mousePosition[1]/100, zPos);

	}

	void OnTriggerEnter(Collider other) {

		selectedCube = other.gameObject;
		if (selectedCube.CompareTag ("Bucket")) {
			GetComponent<Renderer>().material = selectedCube.GetComponent<Renderer>().material;
			templateShape = selectedCube;
		} 
		else if (selectedCube.CompareTag ("Cube")) {
			selectedCube.GetComponent<Renderer> ().material = GetComponent<Renderer> ().material;
		} 
	}
}
