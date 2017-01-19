using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectRemoteControlController : MonoBehaviour
{
	public GameObject cube;
	public float thresholdTop;
	public float thresholdBottom;
    public float thresholdLeft;
    public float thresholdRight;
	public GameObject player;

	private GameObject selectedCube;

	private FollowJoint followJoint; // Kinect Script
	private GameObject selectedBucket;

	void Start() {
		// get Kinect scripts
		followJoint = GetComponent<FollowJoint>();
		selectedCube = player.GetComponent<KinectPlayerController>().selectedCube;
	}

	void FixedUpdate(){

		selectedCube = player.GetComponent<KinectPlayerController>().selectedCube;
		selectedBucket = player.GetComponent<KinectPlayerController>().selectedBucket;
		// read position from Right hand
		transform.position = followJoint.ReadPosition;

		if (transform.position[1] >= thresholdTop && !selectedCube){
			// Left hand is high -> create a new cube
			Vector3 coord = new Vector3((int)player.transform.position[0], (int)player.transform.position[1], (int)player.transform.position[2]);
			GameObject newCube = Instantiate(selectedBucket, coord, new Quaternion(0, 0, 0, 0));
			newCube.tag = "Cube";
			newCube.transform.localScale = selectedBucket.transform.lossyScale * 3;
            //player.selectedCube = null;
		} else if( transform.position[1] <= thresholdBottom && selectedCube.gameObject.CompareTag("Cube") ){
			// Left hand is low -> delete the cube
			player.GetComponent<KinectPlayerController>().selectedCube = null; 
			selectedCube.gameObject.SetActive (false);
		} else if( transform.position[0]<= thresholdLeft && selectedCube.gameObject.CompareTag("Cube")){
            //rotate left
            Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
            selectedCube.transform.rotation = Quaternion.Euler(rot.x, rot.y - 1.0f, 0);
        } else if (transform.position[0] >= thresholdRight && selectedCube.gameObject.CompareTag("Cube")) {
            //rotate right
            Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
            selectedCube.transform.rotation = Quaternion.Euler(rot.x, rot.y + 1.0f, 0);
        }

    }

}
