using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {
	public int enemiesKilled = 0;
	private TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = gameObject.GetComponent<TextMesh> ();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enemyKilled() {
		enemiesKilled++;
		textMesh.text = "Ralphs Bullied: " + enemiesKilled;
	}
}
