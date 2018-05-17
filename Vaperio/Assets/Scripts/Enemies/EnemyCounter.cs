using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {
	public int enemiesKilled = 0;
    public LevelManager levelManager;
	public TextMesh ralphCounterText;


    void Start () {
		ralphCounterText = gameObject.GetComponent<TextMesh> ();
        GameObject foreground = GameObject.Find("Foreground");
        levelManager = foreground.GetComponent<LevelManager>();
    }

	public void enemyKilled() {
		enemiesKilled++;
		ralphCounterText.text = "Ralphs Bullied: " + enemiesKilled;
        levelManager.EnemyKilled();
	}
}
