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
    public AudioClip damageSound;
    private AudioSource[] source;
	private CameraControl cameraControl;

	void Start () {
        source = GetComponents<AudioSource>();
        currentHealth = startingHealth;
		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
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
		if(other.gameObject.layer == LayerMask.NameToLayer("EnemyBullets")){
			BulletMovement bullet =  other.GetComponent<BulletMovement>();
			ApplyDamage(bullet.bulletDamage);
			bullet.ReturnToPool();
		}
		if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemies"))) {
			ApplyDamage(5);
		}
    }
        
    
    private void ApplyDamage (int damage){
        currentHealth -= damage;
		timeSinceHit = 0f;
		CheckForDeath();
		SetColour (hitColour);
        source[1].PlayOneShot(damageSound,1.5f);
    }
    
    private void CheckForDeath() {
		if (currentHealth <= 0) {
			Destroy (this.gameObject);
			SceneManager.LoadScene ("score-screen");
		} else if (currentHealth <= 10) {
			IncreaseCameraEffects ();
		}
	}

	private void IncreaseCameraEffects() {
		cameraControl.IncreaseCameraEffects ();
	}

	private void SetColour (Color colour) {
		foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
			spriteRenderer.color = colour;
		}
	}
}
