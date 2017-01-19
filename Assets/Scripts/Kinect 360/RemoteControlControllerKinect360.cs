using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoteControlControllerKinect360 : MonoBehaviour
{
    public GameObject cube;
    public float thresholdTop;
    public float thresholdBottom;
    public GameObject player;

    private GameObject selectedCube;
    
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandLeft;


    void Start() {
        selectedCube = player.GetComponent<PlayerControllerKinect>().selectedCube;
    }

    void FixedUpdate()
    {

        selectedCube = player.GetComponent<PlayerControllerKinect>().selectedCube;

        KinectManager manager = KinectManager.Instance;
        int iJointIndex = (int)TrackedJoint;

        if (manager.IsUserDetected())
        {

            uint userId = manager.GetPlayer1ID();

            if (manager.IsJointTracked(userId, iJointIndex))
            {
                Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                if (posJoint != Vector3.zero)
                {
                    // read position from Right hand
                    transform.position = posJoint * 10;
                    transform.position = new Vector3(transform.position[0], transform.position[1], transform.position[2] - 15);

                    print(selectedCube);
                    if (transform.position[1] >= thresholdTop && !selectedCube)
                    {
                        //print("CREATE");
                        // Left hand is high -> create a new cube
                        Vector3 coord = new Vector3((int)player.transform.position[0], ((int)player.transform.position[1]) + 0.5f, (int)player.transform.position[2]);
                        GameObject newCube = Instantiate(cube, coord, new Quaternion(0, 0, 0, 0));
                        newCube.GetComponent<Renderer>().material = player.GetComponent<Renderer>().material;
                    }
                    else if (transform.position[1] <= thresholdBottom )
                    {
                        //if(selectedCube.gameObject.CompareTag("Cube"))
                        print("DELETE");
                        print(selectedCube);
                        

                        // Left hand is low -> delete the cube
                        player.GetComponent<PlayerControllerKinect>().selectedCube = null;
                        selectedCube.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
