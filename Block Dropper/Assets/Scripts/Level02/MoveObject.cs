using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

	public float MoveSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > -1) {
			transform.Translate (MoveSpeed * Vector3.left * Time.deltaTime, Space.World);
		} else {
			gameObject.SetActive (false);
		}	
	}
}
