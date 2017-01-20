using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectPlayerController : MonoBehaviour
{
    private FollowJoint followJoint; // Kinect Script
    public GameObject selectedCube;
    public GameObject selectedShape;

    void Start() {
        // get Kinect scripts
        followJoint = GetComponent<FollowJoint>();
    }

    void Update() {
        // read position from Left hand
        // transform.position = followJoint.ReadPosition;
        
        if (followJoint.GeneratedPosition[1] < 0.5) {
            transform.position = new Vector3(followJoint.GeneratedPosition[0], 0.5f, followJoint.GeneratedPosition[2]);
        }
        else {
            transform.position = followJoint.GeneratedPosition;
        }
    }

    void OnTriggerEnter(Collider other) {

        //selectedCube = other.gameObject;
        if (other.gameObject.CompareTag("Bucket") || other.gameObject.CompareTag("Ground Template Object"))
        {
            // collect the material and shape
            selectedShape = other.gameObject;
			GetComponent<Renderer>().material = selectedShape.GetComponent<Renderer>().material;
			
        }
        else if (other.gameObject.CompareTag("Clone Object")){
            // assign the material to a cube
            selectedCube = other.gameObject;
            other.gameObject.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        }
        

    }

    void OnTriggerExit(Collider other){
		selectedCube = null;
	}
}
