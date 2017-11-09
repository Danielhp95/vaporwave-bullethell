using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void flipWorld() {
		transform.Rotate (0f, 180f, 0f);
	}
}
