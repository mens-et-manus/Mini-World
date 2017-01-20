using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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
	private GameObject selectedShape;

	void Start() {
		// get Kinect scripts
		followJoint = GetComponent<FollowJoint>();
		selectedCube = player.GetComponent<KinectPlayerController>().selectedCube;
	}

    /**
     * check whether the coord is available to place a new object 
     * return true if coord is empty space within radius 0.3f
     *        false otherwise
     */
    bool availableSpace( Vector3 coord) {
        float scaleFactor = selectedShape.transform.localScale[0];
        float r = 0.3f;
        if (scaleFactor < 0.01)
        {
            r = 1.5f;
        }
        print(r);
        return !Physics.CheckSphere(coord, r);
    }

    /**
     * Find coordinate that we should place the cloned object based on player's left hand position and object category
     * return   player left hand position for "Floating Template Object"
     *          player left hand position with y=0 (placed on the ground) for "Ground Template Object"
     */
    Vector3 getPlacingCoordinate() {
        int yPosition = (int)player.transform.position[1];

        if (selectedShape.CompareTag("Ground Template Object"))
        {
            yPosition = 0;
        }

        Vector3 placingCoord = new Vector3((int)player.transform.position[0], yPosition, (int)player.transform.position[2]);
        return placingCoord;
    }

    void Update(){

		selectedCube = player.GetComponent<KinectPlayerController>().selectedCube;
        selectedShape = player.GetComponent<KinectPlayerController>().selectedShape;
		// read position from Right hand
		transform.position = followJoint.GeneratedPosition;
        Vector3 rightShoulder = GameObject.Find("Empty Right Shoulder").GetComponent<FollowJoint>().GeneratedPosition;
        Vector3 head = GameObject.Find("Empty Head").GetComponent<FollowJoint>().GeneratedPosition;
        Vector3 rightHand = transform.position;

        float errorFromFaceSize = 1.5f;
        float error = 2f;

        if (transform.position[1] >= thresholdTop && !selectedShape.CompareTag("Bucket") && ( Math.Abs(rightHand[0] - head[0]) > error || Math.Abs(rightHand[2] - head[2]) > error) ) {

            Vector3 placingCoord = getPlacingCoordinate();

            if (availableSpace(placingCoord)) {
                // place an object if no object is at the same position (or too close)
                GameObject newCube = Instantiate(selectedShape, placingCoord, new Quaternion(0, 0, 0, 0));
                newCube.tag = "Clone Object";
                newCube.transform.localScale = selectedShape.transform.lossyScale * 3;
                newCube.transform.rotation = selectedShape.transform.rotation;
            }
           
        } else if ( selectedCube && selectedCube.gameObject.CompareTag("Clone Object"))
        {
            if (Math.Abs(rightHand[0] - head[0]) <= error && Math.Abs(rightHand[2] - head[2]) <= error && rightHand[1] > head[1] + errorFromFaceSize)
            {
                // print("Scale up");
                selectedCube.transform.localScale = selectedCube.transform.localScale * 1.03f;
            }
            else if (Math.Abs(rightHand[0] - head[0]) <= error && Math.Abs(rightHand[2] - head[2]) <= error && rightHand[1] + errorFromFaceSize < head[1])
            {
                //print("Scale down");
                selectedCube.transform.localScale = selectedCube.transform.localScale * 0.99f;
            }
            else if (transform.position[1] <= thresholdBottom) {
                print("delete");
                // (right) hand is low -> delete the cube
                //Destroy(selectedCube);
                player.GetComponent<KinectPlayerController>().selectedCube = null;
                selectedCube.gameObject.SetActive(false);
            } else if (rightShoulder[0] - transform.position[0] >= thresholdLeft) {
                // wipe (right) hand from left to right to rotate obj CW
                Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
                selectedCube.transform.rotation = Quaternion.Euler(rot.x, rot.y - 1.0f, 0);
            } else if (transform.position[0] - rightShoulder[0] >= thresholdRight) {
                // wipe (right) hand from right to left to rotate obj CW
                Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
                selectedCube.transform.rotation = Quaternion.Euler(rot.x, rot.y + 1.0f, 0);
            } 
        }

    }

}
