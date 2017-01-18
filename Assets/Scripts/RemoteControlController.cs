﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoteControlController : MonoBehaviour
{
	public GameObject cube;
	public GameObject player;

	private GameObject selectedCube;
	private GameObject templateShape;


	void Start() {
		selectedCube = player.GetComponent<PlayerController>().selectedCube;
	}

	void FixedUpdate()
	{	// before phy calculation

		selectedCube = player.GetComponent<PlayerController>().selectedCube;

		templateShape = player.GetComponent<PlayerController>().templateShape;

		if(Input.GetKeyDown(KeyCode.P)) {
			// create a new cube when press "P" 
			Vector3 coord = new Vector3((int)player.transform.position[0],((int)player.transform.position[1])+0.5f, (int)player.transform.position[2]);
			GameObject newObj = Instantiate (templateShape, coord, new Quaternion(0,0,0,0));
			print (newObj.tag);
			newObj.tag = "Cube";
		} else if( Input.GetKeyDown(KeyCode.O) && selectedCube.CompareTag("Cube") ){
			// delete the cube when press "O" 
			selectedCube.SetActive (false);
		}

	}

}
