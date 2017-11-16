using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private AmbientCameraMovements ambientMovement;
	private CRTShader crtShader;
	private bool isNetherJumping = false;
	private bool isDivingDown = false;
	public float distortionRate = 0.4f;

	void Start () {
		ambientMovement = GameObject.Find ("Main Camera").GetComponent<AmbientCameraMovements> ();
		crtShader = GameObject.Find ("Main Camera").GetComponent<CRTShader> ();
	}

	void Update() {
		if (isNetherJumping) {
			UpdateDistortion ();
		}
	}

	private void UpdateDistortion () {
		if (isDivingDown) {
			crtShader.Distortion += crtShader.Distortion * (distortionRate * Time.deltaTime);
		} else {
			crtShader.Distortion -= crtShader.Distortion * (distortionRate * Time.deltaTime);
		}
	}

	public void BeginNetherJump() {
		isNetherJumping = true;
		isDivingDown = true;
	}

	public void EndNetherJump () {
		isNetherJumping = false;
	}

	public void ToggleWorld() {
		isDivingDown = false;
		ToggleCRTEffects ();
	}

	private void ToggleCRTEffects() {
		crtShader.Distortion = -1 * crtShader.Distortion;
	}

	public void IncreaseCameraEffects() {
		IncreaseMovement ();
		IncreaseCRTEffects ();
	}

	private void IncreaseMovement() {
		ambientMovement.spaceSpeed = ambientMovement.spaceSpeed * 2.5f;
		ambientMovement.postionWidths = ambientMovement.postionWidths * 2f;
		ambientMovement.rotationSpeed = ambientMovement.rotationSpeed * 2.5f;
		ambientMovement.rotationWidths = ambientMovement.rotationWidths * 2f;
	}

	private void IncreaseCRTEffects() {
		distortionRate = 5f;
		crtShader.scanSize = CRTScanLinesSizes.S256;
	}
}
