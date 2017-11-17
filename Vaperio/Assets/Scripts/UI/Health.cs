using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Health : MonoBehaviour {
	TextMesh textMesh;
	SpaceShipHealth spaceShipHealth;

	void Start () {
		spaceShipHealth = GameObject.Find ("spaceship3D").GetComponent<SpaceShipHealth> ();
		textMesh = gameObject.GetComponent<TextMesh> ();;
	}

	void Update () {
		int health = spaceShipHealth.currentHealth;
		textMesh.text = "Juice:" + health;	
	}
}
