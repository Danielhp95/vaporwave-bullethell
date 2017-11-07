using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipHealth : MonoBehaviour {
    
    private int startingHealth;
    private int currentHealth;
    private Color damageColor = new Color(255f,255f,255f, 1f);
    private SpriteRenderer spaceshipRenderer;


	// Use this for initialization
	void Start () {
        this.startingHealth    = 3;
        this.spaceshipRenderer = GetComponent<SpriteRenderer>();
        //spaceshipRenderer.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
