using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControllerKinect360 : MonoBehaviour
{
    public GameObject selectedCube;
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    
    void Start() {
    }


    void FixedUpdate() {
        KinectManager manager = KinectManager.Instance;
        int iJointIndex = (int)TrackedJoint;

        if (manager.IsUserDetected()) {

            uint userId = manager.GetPlayer1ID();

            if (manager.IsJointTracked(userId, iJointIndex)) {
                Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                if (posJoint != Vector3.zero) {
                    
                    transform.position = posJoint*10;
                    //print(transform.position);
                    transform.position = new Vector3(transform.position[0], transform.position[1], transform.position[2]-15);
                }
            }
        }
    
    }

    void OnTriggerEnter(Collider other) {

        selectedCube = other.gameObject;
        if (other.gameObject.CompareTag("Bucket")) {
            // collect the material
            GetComponent<Renderer>().material = other.gameObject.GetComponent<Renderer>().material;
        }
        else if (other.gameObject.CompareTag("Cube")) {
            // assign the material to a cube
            other.gameObject.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        }

    }

    void OnTriggerExit(Collider other) {
        selectedCube = null;
    }
}
