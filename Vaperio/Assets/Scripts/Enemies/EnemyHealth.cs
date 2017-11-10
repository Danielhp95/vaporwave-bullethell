using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    
    public bool isNether;
    public int startingHealth = 100; 
    private int currentHealth;

	// Use this for initialization
	void Start () {
        
        currentHealth = startingHealth;
		
	}
	
	// Update is called once per frame
	void Update () {
        	
	}
    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer==10){
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

    

