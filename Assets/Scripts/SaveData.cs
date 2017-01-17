using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

	private float myVariable;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


//		PlayerPrefs.SetFloat("Player Score", 10.0f);

		myVariable = PlayerPrefs.GetFloat("Player Score");

		print (myVariable);
	}
}
