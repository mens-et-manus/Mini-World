using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    private Vector3 offset;
    private float speed;

	private float rotationOffset;

    void Start () {
        offset = transform.position-player.transform.position;
		rotationOffset = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;

		Vector3 rot = transform.localRotation.eulerAngles;
		if(Input.GetKeyDown(KeyCode.A)) {
			transform.rotation = Quaternion.Euler(rot.x,rot.y-rotationOffset,0);
		} else if(Input.GetKeyDown(KeyCode.D)) {
			transform.localRotation = Quaternion.Euler(rot.x,rot.y+rotationOffset,0);
		} else if(Input.GetKeyDown(KeyCode.W)) {
			transform.rotation = Quaternion.Euler(rot.x-rotationOffset,rot.y,0);
		} else if(Input.GetKeyDown(KeyCode.S)) {
			transform.localRotation = Quaternion.Euler(rot.x+rotationOffset,rot.y,0);
		}


	}
}
