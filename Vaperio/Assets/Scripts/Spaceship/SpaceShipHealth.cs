using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipHealth : MonoBehaviour {
    
    public  int startingHealth = 30;
    public int currentHealth;
	private Color normalColour = Color.white;
	private Color hitColour = Color.red;
	private float timeToNormal = 0.2f;
	private float timeSinceHit = 0.2f;
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	void Start () {
        currentHealth = startingHealth;
		GetSpriteRenderers ();
	}

	private void GetSpriteRenderers() {
		spriteRenderers.Add(gameObject.GetComponent<SpriteRenderer> ());
		List<SpriteRenderer> children = new List<SpriteRenderer> (gameObject.GetComponentsInChildren<SpriteRenderer> ());
		spriteRenderers.AddRange (children);

	}

	void Update () {
		if (!Pause.paused) {
			UpdateColour ();
		}
	}

	private void UpdateColour () {
		if (timeSinceHit < timeToNormal) {
			ColourToNormal ();
		} else if (currentHealth <= 10) {
			Flash ();
		}
	}
    
	private void ColourToNormal() {
		timeSinceHit += Time.deltaTime;
		if (timeSinceHit > timeToNormal) {
			SetColour (normalColour);
		}
	}

	private void Flash() {
		if (4 < Random.Range (0, 11)) {
			SetColour (hitColour);
		} else {
			SetColour (normalColour);
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
		SetColour (hitColour);
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

	private void SetColour (Color colour) {
		foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
			spriteRenderer.color = colour;
		}
	}
}
