using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {
	public static Lives instance;

	public int maxlives = 5;
	public int currentLives;
	public bool dead = false;
	public int gameOverTime = 10;

	private float timer;


	// Use this for initialization
	void Start () {
		instance = this;
		currentLives = maxlives;

	}
	
	// Update is called once per frame
	void Update () {
		UpdateText ();
		if (dead) {
			timer += Time.deltaTime;
			if (timer >= gameOverTime) {
				UI.instance.txtTitle.gameObject.SetActive (false);

				timer = 0;
				dead = false;
				currentLives = maxlives;
				SceneManager.LoadSceneAsync ("MainMenu");
			}
		} else {
			GameOver ();
		}

	}

	public void RemoveLife (){
		currentLives -= 1;
	}

	void UpdateText (){
		UI.instance.txtLives.text = "Lives: " + currentLives;
	}


	public void GameOver () {
		int seconds;
		int minutes;
		seconds = (int)Level2.instance.totalLevelTimer % 60;
		minutes = (int) Level2.instance.totalLevelTimer / 60;


		if (currentLives <= 0) {
			dead = true;
			UI.instance.txtTitle.text = ("Gameover! You made it to level " + Level2.instance.currentLevel + " in "+minutes+":"+seconds);
			UI.instance.txtTitle.gameObject.SetActive (true);

			Spawner2.instance.isSpawning = false;
			Level2.instance.isRunning = false;
		}


	}
}
