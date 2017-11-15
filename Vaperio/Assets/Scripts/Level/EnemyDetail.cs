using System;
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class EnemyDetail : MonoBehaviour, EnemyListener {
		public GameObject prefab;
		private int killedSoFar = 0;
		public int killCountToProgress;
		public int maxToSpawn;
		public GameObject spawner;

		private List<EnemyDetailListener> listeners = new List<EnemyDetailListener>();

		public void StartEncounter() {
			//instantiate and activate enemy spawner

		}

		public void Subscribe(EnemyDetailListener listener) {
			listeners.Add (listener);
		}

		public void EnemyDefeated() {
			if (++killedSoFar == killCountToProgress) {
				foreach (EnemyDetailListener listener in listeners) {
					listener.ThresholdReached ();
				}
			}
		}
	}
}

