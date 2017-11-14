using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreen : MonoBehaviour {

	void Start () {
		int score = ScoreTracker.score;
		TextMesh text = gameObject.GetComponent<TextMesh> ();
		score += 5;
		text.text = score.ToString();
	}
}
