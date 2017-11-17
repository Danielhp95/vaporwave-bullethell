using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreaterToPlayAgain : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown (KeyCode.Greater)
			|| Input.GetKeyDown (KeyCode.T)
		) {
			SceneManager.LoadScene ("simpsonswave");
		}
	}
}
