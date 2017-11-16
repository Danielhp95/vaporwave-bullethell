using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	AmbientCameraMovements ambientMovement;
	CRTShader crtShader;

	void Start () {
		ambientMovement = GameObject.Find ("Main Camera").GetComponent<AmbientCameraMovements> ();
		crtShader = GameObject.Find ("Main Camera").GetComponent<CRTShader> ();
	}

	public void ToggleWorld() {
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

	}
}
