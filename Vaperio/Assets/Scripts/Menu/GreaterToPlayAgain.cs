using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreaterToPlayAgain : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)
			|| Input.GetKeyDown (KeyCode.T)
		) {
			SceneManager.LoadScene ("simpsonswave");
		}
	}
}
