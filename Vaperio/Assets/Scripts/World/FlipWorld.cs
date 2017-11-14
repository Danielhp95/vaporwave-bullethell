using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWorld : MonoBehaviour { 
	public bool isNether { get; private set; }

	void Start () {
		isNether = false;
	}

	public void flipWorld() {
		transform.Rotate (0f, 180f, 0f);
		isNether = !isNether;
	}
}
