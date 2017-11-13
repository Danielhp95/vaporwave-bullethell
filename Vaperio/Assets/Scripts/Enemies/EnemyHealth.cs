using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    
    public bool isNether = false;
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
		if(other.gameObject.layer== LayerMask.NameToLayer("PplBullets")){
            BulletMovement bullet =  other.GetComponent<BulletMovement>();
			if (bullet.isNether == isNether) {
				ApplyDamage(bullet.bulletDamage);
                bullet.ReturnToPool();
			}
        }
    }
    
    private void ApplyDamage (int damage){
        currentHealth -= damage;
        CheckForDeath();
    }
    
    private void CheckForDeath() {
        if(currentHealth <= 0){
            this.gameObject.SetActive(false);
         }
     }
        
        
}

    

