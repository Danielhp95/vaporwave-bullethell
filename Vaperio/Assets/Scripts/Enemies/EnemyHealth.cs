using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    
    public bool isNether = false;
    public int startingHealth = 100; 
    private int currentHealth;
	private EnemyCounter enemyCounter;
	private Color normalColor = Color.white;
	private Color hitColour = Color.red;
	private float timeToNormal = 0.2f;
	private float timeSinceHit = 0.2f;
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
		enemyCounter = GameObject.Find ("RalphCounter").GetComponent<EnemyCounter>();
		GetSpriteRenderers ();
	}

	private void GetSpriteRenderers() {
		spriteRenderers.Add(gameObject.GetComponent<SpriteRenderer> ());
		List<SpriteRenderer> children = new List<SpriteRenderer> (gameObject.GetComponentsInChildren<SpriteRenderer> ());
		spriteRenderers.AddRange (children);

	}
	
	// Update is called once per frame
	void Update () {
		UpdateColour ();
	}

	private void UpdateColour () {
		if (timeSinceHit < timeToNormal) {
			timeSinceHit += Time.deltaTime;
		}
		if (timeSinceHit > timeToNormal) {
			foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
				spriteRenderer.color = normalColor;
			}
		}
	}
    
    void OnTriggerEnter (Collider other) {
		if(other.gameObject.layer==10){
            BulletMovement bullet =  other.GetComponent<BulletMovement>();
			if (bullet.isNether == isNether) {
				ApplyDamage(bullet.bulletDamage);
				Destroy(other.gameObject);
			}
        }
    }
    
    private void ApplyDamage (int damage){
        currentHealth -= damage;
		timeSinceHit = 0f;
        CheckForDeath ();
		SetHitColour ();
    }
    
    private void CheckForDeath () {
        if(currentHealth <= 0){
            Destroy(this.gameObject);
			enemyCounter.enemyKilled ();
         }
     }
        
	private void SetHitColour () {
		foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
			spriteRenderer.color = hitColour;
		}
	}
}

    

