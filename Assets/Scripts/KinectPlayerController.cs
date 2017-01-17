using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectPlayerController : MonoBehaviour
{
	private FollowJoint followJoint; // Kinect Script
	public GameObject selectedCube;
    
    void Start() {
		// get Kinect scripts
        followJoint = GetComponent<FollowJoint>();
    }

	void FixedUpdate() {
		// read position from Right hand
        transform.position = followJoint.ReadPosition;
    }

    void OnTriggerEnter(Collider other) {

		selectedCube = other.gameObject;
        if (other.gameObject.CompareTag("Bucket")){
			// collect the material
            GetComponent<Renderer>().material = other.gameObject.GetComponent<Renderer>().material;
        }
        else if (other.gameObject.CompareTag("Cube")){
			// assign the material to a cube
            other.gameObject.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        }

    }

	void OnTriggerExit(Collider other){
		selectedCube = null;
	}
}
