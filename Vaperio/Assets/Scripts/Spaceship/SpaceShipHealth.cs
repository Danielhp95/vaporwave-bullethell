﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipHealth : MonoBehaviour {
    
    public  int startingHealth = 30;
    public int currentHealth;
    private Color damageColor = new Color(255f,255f,255f, 1f);
    private SpriteRenderer spaceshipRenderer;


	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
        this.spaceshipRenderer = GetComponent<SpriteRenderer>();
        //spaceshipRenderer.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}
    
    
    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer==11){
            BulletMovement bullet =  other.GetComponent<BulletMovement>();
            ApplyDamage(bullet.bulletDamage);
            Destroy(other.gameObject);
            }
        }
        
    
    private void ApplyDamage (int damage){
        currentHealth -= damage;
        CheckForDeath();
    }
    
    private void CheckForDeath() {
        if(currentHealth <= 0){
            Destroy(this.gameObject);
         }
     }
}
