using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour {
	public static Level2 instance;

	public bool isRunning; // if the game is in progress
	public int currentLevel; //Current player level

	public float spawnHeight = 32;
	public float spawnWait = 1.5f;
	public float moveAmount = 0.1f;
	public float moveSpeed = 2f;
	public Vector2 numToGenerate = new Vector2(10,10);
	public int levelWave = 3;

	public float baseSpawnHeight = 32;
	public float baseSpawnWait = 1.5f;
	public float baseMoveAmount = 0.1f;
	public float baseMoveSpeed = 2;
	public Vector2 baseNumToGenerate = new Vector2(10,10);
	public int baseLevelWave = 3; //How many waves you have to survive

	public int currentWave = 1; //Current wave on this level
	public float totalLevelTimer; //Current time playing

	public float waitTime = 0;
	public float endOfLevelWaitTime = 4;
	private bool isEndOfLevel;


	// Use this for initialization
	void Start () {
		instance = this;
		Setup ();
		GenerateLevelDifficulty ();
	}
		
	// Update is called once per frame
	void Update () {
		
		//If player isn't dead
		if (!Lives.instance.dead) {
			// If level is running
			if (isRunning) {
				totalLevelTimer += Time.deltaTime;

				if (currentWave > levelWave) {
					isEndOfLevel = true;
					IsEndOfLevel ();
				}

				if (isEndOfLevel) {
					waitTime += Time.deltaTime;
				} else {
					isEndOfLevel = false;
					IsEndOfLevel ();
				}

				// Wait 4s then level isn't over
				if (waitTime >= endOfLevelWaitTime) {
					isEndOfLevel = false;
					waitTime = 0;
					IsEndOfLevel();
				}
					
			} // end if running
		} else {
			// Reset game to level 1
			isEndOfLevel = true;
			IsEndOfLevel ();
			Setup ();
		}// end if dead

		UpdateText ();

	} // end update
		
	void UpdateText () {
		int seconds;
		int minutes;
		seconds = (int)totalLevelTimer % 60;
		minutes = (int) totalLevelTimer / 60;

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
			
		UI.instance.txtLevel.text = ("Current Level: " + currentLevel);

//		if (isEndOfLevel) {
//			UI.instance.txtTitle.text = ("Level: " + (currentLevel));
//			UI.instance.txtTitle.gameObject.SetActive(true);
//		} else if (!isEndOfLevel) {
//			UI.instance.txtTitle.gameObject.SetActive(false);
//		}
	}


	//If its end of the level
	void IsEndOfLevel () {
		if (isEndOfLevel) {
			isEndOfLevel = true;
			Spawner2.instance.isSpawning = false;
			currentLevel += 1;
			currentWave = 1;
			GenerateLevelDifficulty ();
			ClearScene ();
		} else {
			// Not end of level
			isEndOfLevel = false;
			Spawner2.instance.isSpawning = true;
		}
	}

	//Clear all currently spawned in objects
	public void ClearScene () {
		GameObject[] currentSpawnedObjects = GameObject.FindGameObjectsWithTag ("SpawnedObject");
		for (int i = 0; i < currentSpawnedObjects.Length; i++) {
			Destroy (currentSpawnedObjects[i], endOfLevelWaitTime);
		}
	}

	void GenerateLevelDifficulty () {
		//Every 5 levels change difficulty

//			Debug.Log ("previous stats");
//			Debug.Log ("Wait: " + spawnWait);
//			Debug.Log ("NumtoGenerate: " + numToGenerate);

		if (currentLevel % 2 == 0) {
			if (spawnWait >= 0.9) {
				spawnWait -= 0.05f;
			}

			if (moveSpeed <= 2.5) {
				moveSpeed += 0.1f; 
			}

			if (numToGenerate.y <= Spawner2.instance.spawnLocations.Count - 2) {
				numToGenerate.y += 4;
			}

			if (numToGenerate.x <= Spawner2.instance.spawnLocations.Count - 2) {
				numToGenerate.x += 4;
			}
		}

		if (currentLevel % 6 == 0){
			levelWave += 5;
		}

		//After generating level difficulty set the stats in the spawner script
		Spawner2.instance.spawnHeight = spawnHeight;
		Spawner2.instance.spawnWait = spawnWait;
		Spawner2.instance.moveAmount = moveAmount;
		Spawner2.instance.moveSpeed = moveSpeed;
		Spawner2.instance.numToGenerate = numToGenerate;
	}
		


	public void Setup () {
		isRunning = true;

		currentLevel = 1;

		//Place stats for level 1
		//baseSpawnHeight = 32;
		//baseSpawnWait = 1.5f;
		//baseMoveAmount = 0.1f;
		//baseMoveSpeed = 2;
		//baseNumToGenerate.Set (10, 10);

		//Set them into working variables
		spawnHeight = baseSpawnHeight;
		spawnWait = baseSpawnWait;
		moveAmount = baseMoveAmount;
		moveSpeed = baseMoveSpeed;
		numToGenerate = baseNumToGenerate;
		levelWave = baseLevelWave;


		totalLevelTimer = 0;
		currentWave = 1;
		waitTime = 0;
	}

}
