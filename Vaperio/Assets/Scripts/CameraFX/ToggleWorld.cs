using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWorld : MonoBehaviour {
	private float acceleration = 0.1f;
	private float speed = 0f;
	private bool togglingWorld = false;
	private bool returning = false;
	private float startingPosition = -10f;
	private AmbientCameraMovements ambientCameraMovements;
	private FlipWorld backgroundFlipWorld;
	private FlipWorld foregroundFlipWorld;
    private AudioSource source;
	private List<SpriteRenderer> ottoRenderers = new List<SpriteRenderer>();
	private CameraControl cameraControl;

	void Start () {
		GameObject camera = GameObject.Find ("Main Camera");
		ambientCameraMovements = camera.GetComponent<AmbientCameraMovements> ();
		GameObject background = GameObject.Find ("Background");
		backgroundFlipWorld = background.GetComponent<FlipWorld> ();
		GameObject foreground = GameObject.Find ("Foreground");
		foregroundFlipWorld = foreground.GetComponent<FlipWorld> ();
		source = GetComponent<AudioSource>();
		GameObject otto = GameObject.Find ("Otto");
		ottoRenderers.AddRange(otto.GetComponents<SpriteRenderer> ());
		ottoRenderers.AddRange(otto.GetComponentsInChildren<SpriteRenderer> ());
		cameraControl = camera.GetComponent<CameraControl> ();
	}

	void Update () {
		if (( Input.GetKeyDown (KeyCode.H) || Input.GetKeyDown (KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
			&& ! togglingWorld) {
            playSound();
			StartToggle ();
		}
		if (togglingWorld) {
			accelerate ();
			move ();
			checkForReverse ();
			if (returning && CheckForEnd()) {
				EndToggle ();
			}
		}
	}

	private void accelerate() {
		speed = speed + acceleration * Time.deltaTime;
	}

	private void move() {
		transform.Translate (0f, 0f, speed);
	}

	private void checkForReverse() {
		if (transform.position.z > 0f) {
			EnterOtherWorld ();
		}
	}

	private void EnterOtherWorld() {
		speed = -speed;
		backgroundFlipWorld.flipWorld ();
		foregroundFlipWorld.flipWorld ();
		transform.Translate (0f, 0f, -2 * transform.position.z);
		returning = true;
		cameraControl.ToggleWorld ();
	}

	private bool CheckForEnd() {
		return transform.position.z < startingPosition || speed > 0;
	}

	private void StartToggle() {
		togglingWorld = true;
		Pause.paused = true;
		ToggleOttoEnabled (true);
	}

	private void EndToggle() {
		speed = 0;
		togglingWorld = false;
		returning = false;
		returnToPosition ();
		Pause.paused = false;
		ToggleOttoEnabled (false);
	}

	private void ToggleOttoEnabled(bool enabled) {
		foreach (SpriteRenderer ottoRenderer in ottoRenderers) {
			ottoRenderer.enabled = enabled;
		}
	}

	private void returnToPosition() {
		float toTravel = startingPosition - transform.position.z;
		transform.Translate (0f, 0f, toTravel);
	}
    
    private void playSound() {
    source.Play();
    }
}

