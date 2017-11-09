using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWorld : MonoBehaviour {
	private float acceleration = 0.1f;
	private float speed = 0f;
	private bool togglingWorld = false;
	private bool returning = false;
	private float startingPosition = -9f;
	private AmbientCameraMovements ambientCameraMovements;
	private FlipWorld backgroundFlipWorld;
	private FlipWorld foregroundFlipWorld;

	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.Find ("Main Camera");
		ambientCameraMovements = camera.GetComponent<AmbientCameraMovements> ();
		GameObject background = GameObject.Find ("Background");
		backgroundFlipWorld = background.GetComponent<FlipWorld> ();
		GameObject foreground = GameObject.Find ("Foreground");
		foregroundFlipWorld = foreground.GetComponent<FlipWorld> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && ! togglingWorld) {
			togglingWorld = true;
			ambientCameraMovements.toggleTogglingWorlds ();
		}
		if (togglingWorld) {
			accelerate ();
			move ();
			checkForReverse ();
			if (returning) {
				checkForEnd ();
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
			speed = -speed;
			backgroundFlipWorld.flipWorld ();
			foregroundFlipWorld.flipWorld ();
			transform.Translate (0f, 0f, -2 * transform.position.z);
			returning = true;
		}
	}

	private void checkForEnd() {
		if (transform.position.z < startingPosition || speed > 0) {
			speed = 0;
			togglingWorld = false;
			returning = false;
			returnToPosition ();
			ambientCameraMovements.toggleTogglingWorlds ();
		}
	}

	private void returnToPosition() {
		float toTravel = startingPosition - transform.position.z;
		transform.Translate (0f, 0f, toTravel);
	}
}
