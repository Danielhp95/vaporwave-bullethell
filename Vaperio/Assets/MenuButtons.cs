using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.J) ) {
			Pause.paused = false;
			Play ();
		}
	}

	public void Play() {
		SceneManager.LoadScene ("simpsonswave");
	}
}
