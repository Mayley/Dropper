using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour {
	public static LevelTimer instance;

	public bool isRunning = true;
	private float levelTimer = 0f;

	// Use this for initialization
	void Start () {
		instance = this;
		levelTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning) {
			levelTimer += Time.deltaTime;

			updateText ();
		} else { 
			ResetTimer ();
		}
	}

	void updateText () {
		int seconds;
		int minutes;
		seconds = (int)levelTimer % 60;
		minutes = (int)levelTimer / 60;

		//Debug.Log ("Seconds: " + seconds);
		//Debug.Log ("Minutes: " + minutes);
		if (minutes < 9) {
			if (seconds < 10) {
				UI.instance.txtTimer.text = ("0" + minutes + ":0" + seconds);
			} else {
				UI.instance.txtTimer.text = ("0" + minutes + ":" + seconds);
			}
		} else {
			if (seconds < 10) {
				UI.instance.txtTimer.text = (minutes + ":0" + seconds);
			} else {
				UI.instance.txtTimer.text = (minutes + ":" + seconds);
			}
		}
	
	}

	public void ResetTimer () {
		levelTimer = 0f;
	}
}
