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
        return !Physics.CheckSphere(coord, 0.3f);
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


        if (transform.position[1] >= thresholdTop) {

            Vector3 placingCoord = getPlacingCoordinate();

            if (availableSpace(placingCoord)) {
                // place an object if no object is at the same position (or too close)
                GameObject newCube = Instantiate(selectedShape, placingCoord, new Quaternion(0, 0, 0, 0));
                newCube.tag = "Clone Object";
                newCube.transform.localScale = selectedShape.transform.lossyScale * 3;
            }
           
        } else if ( selectedCube && selectedCube.gameObject.CompareTag("Clone Object")) {
            if (transform.position[1] <= thresholdBottom){
                // Left hand is low -> delete the cube
                player.GetComponent<KinectPlayerController>().selectedCube = null;
                selectedCube.gameObject.SetActive(false);
            } else if (transform.position[0] <= thresholdLeft ) {
                //rotate left
                Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
                selectedCube.transform.rotation = Quaternion.Euler(rot.x, rot.y - 1.0f, 0);
            } else if (transform.position[0] >= thresholdRight ) {
                //rotate right
                Vector3 rot = selectedCube.transform.localRotation.eulerAngles;
                selectedCube.transform.rotation = Quaternion.Euler(rot.x, rot.y + 1.0f, 0);
            }
        }

    }

}
