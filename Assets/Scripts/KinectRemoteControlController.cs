using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectRemoteControlController : MonoBehaviour
{
	public GameObject cube;
	public float thresholdTop;
	public float thresholdBottom;
	public GameObject player;

	private GameObject selectedCube;

	private FollowJoint followJoint; // Kinect Script
	private GameObject templateShape;

	void Start() {
		// get Kinect scripts
		followJoint = GetComponent<FollowJoint>();
		selectedCube = player.GetComponent<KinectPlayerController>().selectedCube;
	}

	void FixedUpdate(){

		selectedCube = player.GetComponent<KinectPlayerController>().selectedCube;
		templateShape = player.GetComponent<PlayerController>().templateShape;
		// read position from Right hand
		transform.position = followJoint.ReadPosition;

		if (transform.position[1] >= thresholdTop && !selectedCube){
			// Left hand is high -> create a new cube
			Vector3 coord = new Vector3((int)player.transform.position[0], 0, (int)player.transform.position[2]);
			GameObject newCube = Instantiate(templateShape, coord, new Quaternion(0, 0, 0, 0));
			newCube.tag = "Cube";
			newCube.transform.localScale = templateShape.transform.lossyScale * 3;
		} else if( transform.position[1] <= thresholdBottom && selectedCube.gameObject.CompareTag("Cube") ){
			// Left hand is low -> delete the cube
			player.GetComponent<KinectPlayerController>().selectedCube = null; 
			selectedCube.gameObject.SetActive (false);
		}

	}

}
