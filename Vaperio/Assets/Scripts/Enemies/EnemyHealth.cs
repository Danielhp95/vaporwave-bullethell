using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    
    public bool isNether = false;
    public int startingHealth = 100; 
    private int currentHealth;
	private EnemyCounter enemyCounter;
	private Color normalColour = Color.white;
	private Color hitColour = Color.red;
    private Color deathColour = Color.grey;
	private float timeToNormal = 0.2f;
	private float timeSinceHit = 0.2f;
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public AudioClip deathSound;
    private AudioSource deathAudio;
    private Ralph ralph;
    private BoxCollider collider;
    private bool isDying = false;

	void Start () {
        deathAudio = GetComponent<AudioSource>();
        ralph = GetComponent<Ralph>();
        collider = GetComponent<BoxCollider>();
        currentHealth = startingHealth;
		enemyCounter = GameObject.Find ("RalphCounter").GetComponent<EnemyCounter>();
		GetSpriteRenderers ();
	}

	private void GetSpriteRenderers() {
		spriteRenderers.Add(gameObject.GetComponent<SpriteRenderer> ());
		List<SpriteRenderer> children = new List<SpriteRenderer> (gameObject.GetComponentsInChildren<SpriteRenderer> ());
		spriteRenderers.AddRange (children);

	}

	void Update () {
		if (!Pause.paused && !isDying) {
			UpdateColour ();
		}
        if(isDying) {
            SetColour(deathColour);
        }
	}

	private void UpdateColour () {
		if (timeSinceHit < timeToNormal) {
			timeSinceHit += Time.deltaTime;
		}
		if (timeSinceHit > timeToNormal) {
			SetColour(normalColour);
		}
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
        if(isDying == false){
        currentHealth -= damage;
		timeSinceHit = 0f;
        CheckForDeath ();
		SetColour (hitColour);
        }
    }
    
    private void CheckForDeath () {
        if(currentHealth <= 0 && !isDying){
            isDying = true;
            deathAudio.pitch= (Random.Range(0.8f,1.2f));
            deathAudio.PlayOneShot(deathSound, Random.Range(0.8f,1.0f));
            ralph.enabled = false;
            collider.enabled = false;
            Destroy(this.gameObject,deathSound.length);
            enemyCounter.enemyKilled ();
        }
     }
        
	private void SetColour (Color colour) {
		foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
			spriteRenderer.color = colour;
		}
	}
}

    

