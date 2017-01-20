using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KinectCameraController : MonoBehaviour
{

    // Use this for initialization
    public GameObject player;

    // Position Threshold for control
    public float zoomInZDiffThreshold;
    public float zoomOutZDiffThreshold;
    public float rotationXDiffShouldersError;

    // Camera Animation settings
    private Vector3 offset;
    private float speed;
    private float rotationOffset;
    
    // Joint locations
    private Vector3 leftHand;
    private Vector3 rightHand;
    private Vector3 rightShoulder;
    private Vector3 leftShoulder;

    // Note Z-index: near camera = very small value ex.-15
    // the far the more z value

    void Start()
    {
        offset = transform.position - player.transform.position;
        rotationOffset = .5f;
    }

    // Update is called once per frame
    void Update()
    {

        float errorOffset = 0.5f;
        transform.position = player.transform.position + offset;
        leftHand = GetComponent<BodyTracker>().leftHand;
        rightHand = GetComponent<BodyTracker>().rightHand;
        rightShoulder = GetComponent<BodyTracker>().rightShoulder;
        leftShoulder = GetComponent<BodyTracker>().leftShoulder;

        if (leftHand == null || rightHand == null || rightShoulder == null || leftShoulder == null ) {
            return;
        }

        Vector3 rot = transform.localRotation.eulerAngles;

        if ( Math.Abs(leftShoulder[0] - rightShoulder[0]) <= rotationXDiffShouldersError && leftShoulder[2] >= rightShoulder[2] + errorOffset)
        {
            // turn body to the left to rotate camera CW
            transform.rotation = Quaternion.Euler(rot.x, rot.y - rotationOffset, 0);
        }
        else if (Math.Abs(leftShoulder[0] - rightShoulder[0]) <= rotationXDiffShouldersError && rightShoulder[2] >= leftShoulder[2] + errorOffset)
        {
            // turn body to the right rotate camera CCW
            transform.localRotation = Quaternion.Euler(rot.x, rot.y + rotationOffset, 0);
        }
        else if (rightShoulder[2] - rightHand[2] >= 0 && rightShoulder[2] - rightHand[2] >= zoomInZDiffThreshold)
        {
            // bring right hand to front to zoom in
            offset = offset * (0.97f);
            transform.position = player.transform.position + offset;
        }
        else if (rightHand[2] - rightShoulder[2] >= 0 && rightHand[2] - rightShoulder[2] >= zoomOutZDiffThreshold)
        {
            // bring right hand to back (behind right shoulder) to zoom out
            offset = offset * (1.03f);
            transform.position = player.transform.position + offset;
        }

    }
}
