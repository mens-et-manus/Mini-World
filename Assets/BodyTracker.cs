using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTracker : MonoBehaviour {

    public Vector3 leftHand;
    public Vector3 rightHand;
    public Vector3 rightShoulder;
    public Vector3 leftShoulder;
    public Vector3 head;
    // Use this for initialization

    void fetchJointPositions() {
        leftHand = GameObject.Find("Player").GetComponent<KinectPlayerController>().GetComponent<FollowJoint>().GeneratedPosition;
        rightHand = GameObject.Find("RemoteControl").GetComponent<KinectRemoteControlController>().GetComponent<FollowJoint>().GeneratedPosition;
        rightShoulder = GameObject.Find("Empty Right Shoulder").GetComponent<FollowJoint>().GeneratedPosition;
        leftShoulder = GameObject.Find("Empty Left Shoulder").GetComponent<FollowJoint>().GeneratedPosition;
        head = GameObject.Find("Empty Head").GetComponent<FollowJoint>().GeneratedPosition;
    }

    void Start () {
        fetchJointPositions();
    }

    // Update is called once per frame
    void Update () {
        fetchJointPositions();
    }
}
