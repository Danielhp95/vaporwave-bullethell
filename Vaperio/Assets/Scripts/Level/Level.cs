using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class Level : MonoBehaviour, EncounterListener  {
	public List<Encounter> encounters = new List<Encounter>();
	private int currentEncounterIndex = 0;
	private Encounter currentEncounter;
	private bool isComplete = false;

	void Start () {
		if (encounters.Count > 0) {
			StartEncounter ();
		}
	}

	void Update () {

	}

	public void EncounterCompleted() {
		EndEncounter ();
		if (!isComplete) {
			StartEncounter ();
		} else {
			CompleteLevel ();
		}
	}

	private void EndEncounter() {
		currentEncounter.EndEncounter ();
		currentEncounterIndex++;
		isComplete = currentEncounterIndex == encounters.Count;
	}

	private void StartEncounter() {
		currentEncounter = encounters [currentEncounterIndex];
		currentEncounter.Subscribe ();
		currentEncounter.StartEncounter ();
	}

	private void CompleteLevel() {
		SceneManager.LoadScene ("menu");
	}
}
