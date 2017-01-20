using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectPlayerController : MonoBehaviour
{
	private FollowJoint followJoint; // Kinect Script
	public GameObject selectedCube;
	public GameObject selectedBucket;

    void Start() {
		// get Kinect scripts
        followJoint = GetComponent<FollowJoint>();
    }

	void FixedUpdate() {
		// read position from Right hand
        //transform.position = followJoint.ReadPosition;
        if (followJoint.GeneratedPosition[1] < 0.5){
            transform.position = new Vector3 (followJoint.GeneratedPosition[0],0.5f,followJoint.GeneratedPosition[2]);
        }
        else { transform.position = followJoint.GeneratedPosition; }
    }

    void OnTriggerEnter(Collider other) {

		//selectedCube = other.gameObject;
        if (other.gameObject.CompareTag("Bucket")){
            // collect the material and shape
            selectedBucket = other.gameObject;
			GetComponent<Renderer>().material = selectedBucket.GetComponent<Renderer>().material;
			
        }
        else if (other.gameObject.CompareTag("Cube")){
            // assign the material to a cube
            selectedCube = other.gameObject;
            other.gameObject.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        }

    }

	void OnTriggerExit(Collider other){
		selectedCube = null;
	}
}
