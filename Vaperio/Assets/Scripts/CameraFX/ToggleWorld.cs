using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWorld : MonoBehaviour {
	private float acceleration = 0.1f;
	private float speed = 0f;
	private bool togglingWorld = false;
	private float startingPosition = -9f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			togglingWorld = true;
		}
		if (togglingWorld) {
			accelerate ();
			move ();
			checkForReverse ();
			checkForEnd ();
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
			transform.Translate (0f, 0f, -2 * transform.position.z);
		}
	}

	private void checkForEnd() {
		if (transform.position.z < startingPosition) {
			speed = 0;
			togglingWorld = false;
			returnToPosition ();
		}
	}

	private void returnToPosition() {
		float toTravel = startingPosition - transform.position.z;
		transform.Translate (0f, 0f, toTravel);
	}
}
