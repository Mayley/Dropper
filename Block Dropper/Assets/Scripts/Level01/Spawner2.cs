using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour {
	public static Spawner2 instance;

	public bool isSpawning = false;

	public List<GameObject> spawnObjects;
	public List<GameObject> spawnLocations;
	private GameObject spawnObject;
	private Vector3 spawnLocation;

	public float spawnHeight;
	public float spawnWait;
	public float moveAmount;
	public float moveSpeed;

	private float timeSinceLastSpawn;

	public Vector2 numToGenerate;

	// Use this for initialization
	void Awake () {
		instance = this;

		//Dont spawn straight away
		//Get all spawn locations
		isSpawning = false;

		if (spawnLocations.Count <= 0) {
			GetSpawnLocations ();	
		}

		timeSinceLastSpawn = 0;

		//Populate objects
		RandomSpawnLocation ();
		RandomObject();

	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastSpawn += Time.deltaTime;

		if (isSpawning && timeSinceLastSpawn >= spawnWait) {
			SpawnObject ();
			timeSinceLastSpawn = 0;
		}

	}

	// Populate spawnLocations with all objects that have tag "Spawner"
	void GetSpawnLocations () {
		spawnLocations = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Spawner"));

		//Debug.Log ("Spawn points: " + spawnLocations.Count);
		//Debug.Log ("Spawner Tags: " + GameObject.FindGameObjectsWithTag ("Spawner").Length);

	}


	// Get random spawn location from list of spawn locations
	void RandomSpawnLocation () {
		
		int i = Random.Range (0, spawnLocations.Count);
		spawnLocation = spawnLocations [i].gameObject.transform.position;
		spawnLocation.y += spawnHeight;

	}

	// Get random object to drop
	void RandomObject () {

		int i = Random.Range (0, spawnObjects.Count);
		spawnObject = spawnObjects [i];

	}
		

	void SpawnObject () {
		//Debug.Log ("Spawning Object");
		List<Vector3> previousSpawnLocations = new List<Vector3>();
		previousSpawnLocations.Clear ();




		//Spawn Object
		for (int i = 1; i <= numToGenerate.y; i++ ){
			


			// Debug.Log ("Num of previous spawn locations: "+previousSpawnLocations.Count);

			// Check to see if the spawn location has been used before. If it has generate a new one
			if (previousSpawnLocations.Count != 0) {
				for (int x = 0; x < previousSpawnLocations.Count; x++) {

					if (previousSpawnLocations [x] == spawnLocation) {
						RandomSpawnLocation ();
						x = 0;
					}
				}
			} 


			GameObject spawnedObject = Instantiate (spawnObject, spawnLocation, spawnObject.transform.rotation);
			spawnedObject.transform.parent = GameObject.FindGameObjectWithTag ("SpawnedObjectsParent").gameObject.transform;

			//Added spawned location to list
			previousSpawnLocations.Add (spawnLocation);

			//Get new Object & Spawn Location
			RandomSpawnLocation();
			RandomObject();
		}


		//Debug.Log ("Wave: " + Level2.instance.currentWave + " / " + Level2.instance.levelWave);
		Level2.instance.currentWave++;

	}


}
