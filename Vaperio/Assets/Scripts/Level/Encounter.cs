using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class Encounter : MonoBehaviour, EnemyDetailListener {
	private List<EncounterListener> listeners = new List<EncounterListener>();
	public List<EnemyDetail> enemyDetails = new List<EnemyDetail>();
	private int thresholdsReached = 0;

	void Start () {
		
	}

	void Update () {
		
	}

	public void StartEncounter() {
		foreach (EnemyDetail enemyDetail in enemyDetails) {
			enemyDetail.StartEncounter ();
		}
	}

	public void EndEncounter() {
		listeners.Clear ();
	}

	public void Subscribe(EncounterListener listener) {
		listeners.Add (listener);
	}

	public void ThresholdReached() {
		if (++thresholdsReached == enemyDetails.Count) {
			foreach (EncounterListener listener in listeners) {
				listener.EncounterCompleted ();
			}
		}
	}
}
