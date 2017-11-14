using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipHealth : MonoBehaviour {
    
    public  int startingHealth = 30;
    public int currentHealth;
	private Color normalColor = Color.white;
	private Color hitColour = Color.red;
	private float timeToNormal = 0.2f;
	private float timeSinceHit = 0.2f;
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();


	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
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
    
    
    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer==11){
            BulletMovement bullet =  other.GetComponent<BulletMovement>();
            ApplyDamage(bullet.bulletDamage);
            Destroy(other.gameObject);
            }
        }
        
    
    private void ApplyDamage (int damage){
        currentHealth -= damage;
		timeSinceHit = 0f;
		CheckForDeath();
		SetHitColour ();
    }
    
    private void CheckForDeath() {
		if (currentHealth <= 0) {
			Destroy (this.gameObject);
			SceneManager.LoadScene ("menu");
		} else if (currentHealth <= 10) {
			IncreaseCameraSpeed ();
		}
	}

	private void IncreaseCameraSpeed() {
		AmbientCameraMovements ambientMovement = GameObject.Find ("Main Camera").GetComponent<AmbientCameraMovements> ();
		ambientMovement.spaceSpeed = ambientMovement.spaceSpeed * 2.5f;
		ambientMovement.postionWidths = ambientMovement.postionWidths * 2f;
		ambientMovement.rotationSpeed = ambientMovement.rotationSpeed * 2.5f;
		ambientMovement.rotationWidths = ambientMovement.rotationWidths * 2f;
	}

	private void SetHitColour () {
		foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
			spriteRenderer.color = hitColour;
		}
	}
}
