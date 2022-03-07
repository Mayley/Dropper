using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void btnQuit () {
		Application.Quit ();
	}

	public void btnSettings () {
		
	}

	public void btnHelp () {
		SceneManager.LoadSceneAsync ("Level02");	
	}

	public void btnPlay () {
		SceneManager.LoadSceneAsync ("Level01");
	}
}
