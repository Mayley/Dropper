using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandom : MonoBehaviour {

	public Vector2 bounds = new Vector2(-21,21);
	public int maxY = 34;
	public GameObject spawnObject;
	public float animationSpeed = 2;

	public int pooledAmount = 20;
	List<GameObject> spawnedObjects;

	private List<Vector3> previousSpawnLocation = new List<Vector3>();
	private Vector3 spawnLocation;

	// Use this for initialization
	void Start () {

		//Create all the objects we're going to use
		spawnedObjects = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++){
			GameObject obj = Instantiate(spawnObject);
			//Set parent of object to empty game object
			obj.transform.parent = GameObject.Find ("AnimatedBackground").gameObject.transform;
			//Hide object till needed
			obj.SetActive(false);
			//Add to list
			spawnedObjects.Add(obj);
		}

		//Spawn a new object every X seconds
		InvokeRepeating ("SpawnObject", animationSpeed, animationSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnObject () {

		//Generate new spawn location
		RandomSpawnLocation ();

		//Keep cache of last 3 spawn locations, its its bigger than 3 then remove fist entry
		if (previousSpawnLocation.Count == 3) {
			previousSpawnLocation.RemoveAt (0);
		} 

		//Add new spawn location to list
		previousSpawnLocation.Add (spawnLocation);

		//Look through list of objects, and enable an inactive object
		for (int i = 0; i < spawnedObjects.Count; i++) {
			if (!spawnedObjects [i].activeInHierarchy) {
				spawnedObjects [i].transform.position = spawnLocation;
				spawnedObjects [i].SetActive (true);
				break;	
			}
		}				
	}

	void RandomSpawnLocation () {
		int randomX;

		randomX = (int) Random.Range (bounds.x, bounds.y);
		spawnLocation.x = randomX;
		spawnLocation.y = maxY;
		spawnLocation.z = 0;

		CheckSpawnLocation ();

	}

	void CheckSpawnLocation () {

		//Check if the spawn location of the object is the same as any of the previous spawn locations. If it is then generate a new spawn location and look through list again
		if (previousSpawnLocation.Count != 0) {
			for (int i = 0; i < previousSpawnLocation.Count; i++) {
				if (previousSpawnLocation [i] == spawnLocation) {
					RandomSpawnLocation ();
					i = 0;
				}
			}
		}
	}
}
