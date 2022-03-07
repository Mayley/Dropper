using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnObject : MonoBehaviour {
	public static SpawnObject instance;

	public bool isSpawning = true;
	public int waveSize;
	public float waveTimer;

	private int obstacleList;

	private List<GameObject> spawnLocations = new List<GameObject> ();
	private List<Vector3> previousSpawnLocation = new List<Vector3> ();

	private int rsl;
	private int ro;
	private int width;
	private Vector3 pos;

	// Use this for initialization
	void Awake () {
		instance = this;

		if (spawnLocations.Count <= 0) {
			GetSpawnLocations ();
		}
	}

	void Start () {
		InvokeRepeating ("SpawnObstacle", waveTimer, waveTimer);
	}

	void SpawnObstacle () {
		if (isSpawning) {
			//Spawn Objects
			//Spawning new wave, so clear all previous spawn locations
			previousSpawnLocation.Clear ();

			//Generate objects till waveSize
			for (int i = 0; i < waveSize; i++) {

				// Get random spawn location, and Obstacle
				RandomSpawnLocation ();
				RandomObstacle ();

				//Set pos = SpawnLocation co-ords
				pos = spawnLocations[rsl].transform.position;
				//Add spawn location to used spawn locations
				previousSpawnLocation.Add (pos);

				//Get an object from the object pool with random index (ro)
				GameObject obstacle = ObjectPooling.instance.GetPooledObject (ro);

				if (obstacle != null) {
					//Get width of object, then stop objects spawning within spawnpoints around it.
					int width = (int)obstacle.gameObject.GetComponentInChildren<MeshRenderer> ().bounds.size.z;
					if (width > 0) {
						width -= 1;
					}

					//Check the spawn location isn't used, and that the block doesn't intersect with other blocks
					isSpawnLocationUsed ();

					if (width != 0) {
						int tempIndex = rsl;
						for (int z = rsl; z <= (rsl + width); z++) {
							if (tempIndex < spawnLocations.Count && tempIndex >= 0) {
								previousSpawnLocation.Add (spawnLocations [tempIndex].transform.position);
								tempIndex++;
							} else {
								break;
							}
						}
					}

					//if object is longer than room then spawn in middle
					if (width >= 19) {
						pos.z = 0;
					} 

					//Set spawn location + any changes needed
					obstacle.transform.position = pos;

					//Set object to active
					obstacle.SetActive (true);

				} else {
					//Object is null
					Debug.Log ("Couldn't get object");
				}
			} // end wave spawning 
		} else {
			//Not spawning
		}
	}

	/*
	 * Check the left spawn location
	 * If its used new spawn location & restart
	 * If its not used then check the right bound of the box
	 * If its used then get a new spawn location & restart
	 * if its not used then spawn
	 * 
	 * 
	 * 
	 */

	int findAvailableSpawnAttempts = 0;

	void isSpawnLocationUsed () {
		bool isAvailable = true;;
		List<Vector3> unavailableSpawns = new List<Vector3> ();
		int rightBoundIndex = rsl + width;

		if (previousSpawnLocation.Count != 0) {
			// Loop through all spawn locations for clashes in the spawn location origin
			// If clash is found restart process
			for (int i = 0; i < previousSpawnLocation.Count; i++) {
				if (findAvailableSpawnAttempts <= 5) {
					for (int x = 0; x < unavailableSpawns.Count; x++) {
						if (previousSpawnLocation [i] == spawnLocations [rsl].transform.position && previousSpawnLocation[i] != unavailableSpawns[x]) {
							RandomSpawnLocation ();
							rightBoundIndex = rsl + width;
							isAvailable = false;
							findAvailableSpawnAttempts++;
							i = 0;
						} else {
							//No clash found within the origin
							isAvailable = true;
							break;
						}	
					}
				}// Run through 5 times, no spawn point found give up
				isAvailable = false;
				break;
			}

			if (isAvailable) {
				for (int x = 0; x < previousSpawnLocation.Count; x++) {
					if (previousSpawnLocation [x] == spawnLocations [rightBoundIndex].transform.position) {
						//Right bound is being used run check again
						unavailableSpawns.Add(spawnLocations[rsl].transform.position);
						RandomSpawnLocation ();
						isSpawnLocationUsed ();
					} else {
						//Right bound isn't used so spawnpoint can be used
						isAvailable = true;
					}
				}
			} else {
				//No spawn points available
				isAvailable = false;
			}
		} else {
			//No spawn points to check agaisnt so is available
			isAvailable = true;
		}

	}




	//Check if the spawn location of the object is the same as any of the previous spawn locations.
	//If a clash is found then generate new spawn location and restart loop		
//	void isSpawnLocationUsed () {
//		bool isAvailable = false;
//		Debug.Log ("RSL: " + rsl);
//		Vector3 rightPos = spawnLocations [rsl].transform.position;
//		rightPos.z -= width;
//		//Loop through all previous locations checking for clashes
//		if (previousSpawnLocation.Count != 0) {
//			for (int i = 0; i < previousSpawnLocation.Count; i++) {
//				if (previousSpawnLocation [i] == pos) {
//					//Clash found
//					RandomSpawnLocation ();
//					isAvailable = false;
//					isSpawnLocationUsed ();
//				} else {
//					//Check that the right most part of the object isn't within another object
//					isAvailable = true;
//				}
//			}
//
//
//			if (isAvailable == false) {
//				//No spawn location available
//			}
//		}
//	}

	void RandomObstacle () {
		ro = Random.Range (0, ObjectPooling.instance.obstacles.Count );
	}

	void RandomSpawnLocation () {
		//Generate the index for a random spawn location
		rsl = Random.Range(0, spawnLocations.Count );
	}

	void GetSpawnLocations () {
		//Get all objects with tag Spawner, then order them by name
		spawnLocations = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Spawner").OrderBy( go => go.name));	
	}
}
