using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {
		Lives.instance.RemoveLife ();
	}
}
