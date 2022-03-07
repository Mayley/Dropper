using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateObstacle : MonoBehaviour {
	public static GenerateObstacle instance;

	public bool isGenerating = true;

	public List<GameObject> obstacles;
	public List<int> poolSize;
	public List<int> waveSize;


	public float waveTimer = 0.6f;


	private List<GameObject> spawnLocations = new List<GameObject> ();
	private List<Vector3> previousSpawnLocation = new List<Vector3> ();

	private List<List<GameObject>> obstaclePool = new List<List<GameObject>> ();

	private int rsl;


	// Use this for initialization
	void Awake () {
		instance = this;
		GeneratePooledObstacles ();

		if (spawnLocations.Count <= 0) {
			GetSpawnLocations ();
		}


	}

	void Start () {
		//InvokeRepeating ("SpawnObstacle", waveTimer, waveTimer);  
		InvokeRepeating ("SpawnObstacle", waveTimer, waveTimer);
	}
		

	void SpawnObstacle () {
		if (isGenerating) {
			previousSpawnLocation.Clear ();

			GenerateRandomSpawnLocation ();

			int r = GetRandomObstacle ();

			//Genereate however many objects the game says to generate
			for (int x = 1; x <= waveSize [r]; x++) {
			
				int width = (int)(obstacles [r].gameObject.GetComponentInChildren<MeshRenderer> ().bounds.size.z - 1);

//			Debug.Log ("Object: " + x + "/" + waveSize [r]);
//			Debug.Log ("SpawnLocation Index: " + rsl);
//			Debug.Log ("PrevSpawnLocation Count: " + previousSpawnLocation.Count);
//			Debug.Log ("Width: " + width);
		
				for (int i = 0; i < obstaclePool [r].Count; i++) {
					if (!obstaclePool [r] [i].activeInHierarchy) {
						Vector3 tempPos = spawnLocations [rsl].transform.position;
						if (width >= 19) {
							tempPos.z = 0;	
						}
						obstaclePool [r] [i].transform.position = tempPos;
						obstaclePool [r] [i].SetActive (true);
						break;
					} 
				}
				
				if (width != 0) {
					for (int z = rsl; z <= (rsl + width); z++) {
						if (z < spawnLocations.Count && z > 0) {
							previousSpawnLocation.Add (spawnLocations [z].transform.position);
						}
					}
					for (int z = rsl; z >= (rsl - width); z--) {
						if (z < spawnLocations.Count && z > 0) {
							previousSpawnLocation.Add (spawnLocations [z].transform.position);
						}
					}
				} else {
					previousSpawnLocation.Add (spawnLocations [rsl].transform.position);
				}

				GenerateRandomSpawnLocation ();
			}
		} else {
			Invoke ("DespawnObstacles", 2f);
		}
	}

	void DespawnObstacles () {

		for (int i = 0; i < obstaclePool.Count; i++) {
			for (int x = 0; x < obstaclePool [i].Count; x++) {
				obstaclePool [i] [x].SetActive (false);
			}
		}


	}
		

	int GetRandomObstacle() {
		int i = Random.Range (0, obstacles.Count);
		return i;
	}
		
	void GeneratePooledObstacles () {
		//Create a new list for each obstacle we need a pool for
		for (int x = 0; x < obstacles.Count; x++) {
			obstaclePool.Add (new List<GameObject>(30));	
		}

		int i = 0;

		foreach (List<GameObject> pool in obstaclePool) {

			for (int x = 0; x < poolSize[i]; x++) {
				GameObject obj = Instantiate (obstacles [i]);
				obj.transform.parent = GameObject.Find ("SpawnedObstacles").gameObject.transform;
				obj.name = obstacles [i].name + " " + x;
				obj.SetActive (false);
				pool.Add (obj);
			}
			i++;
		}
	}

	void GetSpawnLocations () {
		spawnLocations = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Spawner").OrderBy( go => go.name));

	}

	void GenerateRandomSpawnLocation () {
		RandomSpawnLocation ();
		CheckSpawnLocation ();
	}

	void RandomSpawnLocation () {
		//Debug.Log ("generate New Spawn");
		int i = Random.Range (0, spawnLocations.Count);
		// Debug.Log ("RandomSpawnLocation Index: "+i);
		rsl = i;
	}

	void CheckSpawnLocation () {
		//Check if the spawn location of the object is the same as any of the previous spawn locations. If it is then generate a new spawn location and look through list again
		if (previousSpawnLocation.Count != 0) {
			for (int i = 0; i < previousSpawnLocation.Count; i++) {
				if (previousSpawnLocation [i] == spawnLocations[rsl].transform.position) {
					//Debug.Log ("Spawn Locaiton was same, generate new spawn");
					RandomSpawnLocation ();
					i = 0;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

