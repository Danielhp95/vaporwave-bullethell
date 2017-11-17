using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	Text text;
	SpaceShipHealth spaceShipHealth;

	void Start () {
		spaceShipHealth = GameObject.Find ("spaceship3D").GetComponent<SpaceShipHealth> ();
		text = gameObject.GetComponent<Text> ();;
	}

	void Update () {
		int health = spaceShipHealth.currentHealth;
		text.text = "Your health:" + health;	
	}
}
