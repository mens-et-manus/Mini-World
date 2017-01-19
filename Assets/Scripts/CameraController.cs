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
		} else if (Input.GetKeyDown (KeyCode.N) ) {
			// zoom in by pressing N
			offset = offset*(0.9f);
			transform.position = player.transform.position + offset;
		} else if (Input.GetKeyDown (KeyCode.M) ) {
			// zoom out by pressing M
			offset = offset*(1.1f);
			transform.position = player.transform.position + offset;
		}

	}
}
