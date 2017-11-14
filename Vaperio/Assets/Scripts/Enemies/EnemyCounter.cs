using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {
	public int enemiesKilled = 0;
	private TextMesh textMesh;

	void Start () {
		textMesh = gameObject.GetComponent<TextMesh> ();		
	}

	public void enemyKilled() {
		enemiesKilled++;
		textMesh.text = "Ralphs Bullied: " + enemiesKilled;
	}
}
