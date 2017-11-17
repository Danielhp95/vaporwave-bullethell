using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {
	void Start () {
		int score = ScoreTracker.score;
		Text text = gameObject.GetComponent<Text> ();
		text.text = "You rejected this many Ralphs:" + score;
		ScoreTracker.score = 0;
	}
}
