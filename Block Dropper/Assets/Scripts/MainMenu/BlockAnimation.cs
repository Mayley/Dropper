using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour {

	public float dropSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.y > -13) {
			transform.Translate (dropSpeed * Vector3.down * Time.deltaTime, Space.World);
		} else {
			gameObject.SetActive (false);
		}
	}
}
