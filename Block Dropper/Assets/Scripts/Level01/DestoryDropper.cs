using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryDropper : MonoBehaviour {

	private float moveSpeed;
	private float moveAmount;
	private Vector3 pos;

	// Use this for initialization
	void Start () {

		moveSpeed = Spawner2.instance.moveSpeed;
		moveAmount = Spawner2.instance.moveAmount;
			
			
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y > 2.5) {
			transform.Translate (Vector3.down * (moveAmount*moveSpeed),Space.World);
		} else {
			Destroy (gameObject);
		}
	}
}
