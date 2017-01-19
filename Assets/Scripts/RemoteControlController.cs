using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoteControlController : MonoBehaviour
{
	public GameObject cube;
	public GameObject player;

	private GameObject selectedCube;
	private GameObject templateShape;

	private float rotationOffset;
	void Start() {
		selectedCube = player.GetComponent<PlayerController>().selectedCube;
		rotationOffset = 90.0f;
	}

	void FixedUpdate()
	{	// before phy calculation

		selectedCube = player.GetComponent<PlayerController>().selectedCube;

		templateShape = player.GetComponent<PlayerController>().templateShape;

		if (Input.GetMouseButtonDown(0)) {
			// create a new cube when click
			//Vector3 coord = new Vector3 ((int)player.transform.position [0], ((int)player.transform.position [1]) + 0.5f, (int)player.transform.position [2]);
			Vector3 coord = new Vector3 ((int)player.transform.position [0], 0, (int)player.transform.position [2]);
			GameObject newObj = Instantiate (templateShape, coord, new Quaternion (0, 0, 0, 0));
			print (newObj.tag);
			newObj.tag = "Cube";
			newObj.transform.localScale = templateShape.transform.lossyScale * 3;

		} else if (Input.GetMouseButtonDown(1) && selectedCube.CompareTag ("Cube")) {
			// delete the cube when right click
			selectedCube.SetActive (false);
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && selectedCube.CompareTag ("Cube")) {
			Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
			selectedCube.transform.rotation=Quaternion.Euler(rot.x,rot.y-rotationOffset,0);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && selectedCube.CompareTag ("Cube")) {
			Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
			selectedCube.transform.rotation=Quaternion.Euler(rot.x,rot.y+rotationOffset,0);
		} else if (Input.GetKeyDown (KeyCode.V) && selectedCube.CompareTag ("Cube")) {
			// enlarge object by pressing V
			selectedCube.transform.localScale = selectedCube.transform.localScale * 1.2f;
		} else if (Input.GetKeyDown (KeyCode.B) && selectedCube.CompareTag ("Cube")) {
			// shrink object by pressing B
			selectedCube.transform.localScale = selectedCube.transform.localScale * 0.8f;
		} 
	}

}
