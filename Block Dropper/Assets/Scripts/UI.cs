using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
	public static UI instance;

	public Text txtTitle;
	public Text txtLives;
	public Text txtTimer;
	public Text txtLevel;
	public Button btnStart;

	// Use this for initialization
	void Awake () {
		instance = this;
			
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r")) {
			Restart ();
		}

	}

	public void MainMenu() {
		SceneManager.LoadSceneAsync ("MainMenu");
	}

	public void isEndless (){
		//Level.instance.isEndless = !Level.instance.isEndless;
	}

	public void Restart (){
		//Lives.instance.gameOver = true;
	}

	public void StartGame(){
		Text btnStartText = btnStart.GetComponentInChildren<Text>();

		if (!Level2.instance.isRunning) {
			Level2.instance.isRunning = true;
			btnStartText.text = "STOP";
		} else {
			btnStartText.text = "START";
			Level2.instance.isRunning = false;
			Spawner2.instance.isSpawning = false;
			Level2.instance.ClearScene ();
			Level2.instance.Setup ();
		}
	}
}
