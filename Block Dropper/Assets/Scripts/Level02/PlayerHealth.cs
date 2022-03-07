using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public static PlayerHealth instance;

	public float pHealth = 100;
	public bool isAlive = true;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void Start () {
		UpdateUI ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DamagePlayer (float dmg){
		if (isAlive) {
			pHealth = pHealth - dmg;
			IsAlive ();
			UpdateUI ();
		}
	}

	public void HealPlayer (float heal){
		if (isAlive) {
			pHealth = pHealth + heal;
			IsAlive ();
			UpdateUI ();
		}
	}

	public void IsAlive () {
		if (pHealth <= 0) {
			isAlive = false;
			//GenerateObstacle.instance.isGenerating = false;
			LevelTimer.instance.isRunning = false;
			UpdateUI ();
			Invoke ("ResetPlayer", 4f);
		} else {
			isAlive = true;
			//GenerateObstacle.instance.isGenerating = true;
			LevelTimer.instance.isRunning = true;
			UpdateUI ();

		}
	}

	void UpdateUI () {
		UI.instance.txtLives.text = "Health: " + pHealth.ToString() + "%";

		if (!isAlive) {
			UI.instance.txtTitle.gameObject.SetActive (true);
		} else {
			UI.instance.txtTitle.gameObject.SetActive (false);
		}
	}

	void ResetPlayer () {
		pHealth = 100;
		isAlive = true;
		IsAlive ();
	}
}
