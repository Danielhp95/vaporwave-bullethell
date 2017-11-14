using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
	public float speed = 0.005f;

	void Start () {
		
	}

	void Update () {
		if (!Pause.paused) {
			Vector2 translation = new Vector2 (speed * Time.deltaTime, 0f);
			var material = GetComponent<Renderer> ().material;
			material.mainTextureOffset = translation + material.mainTextureOffset;
		}
	}
}
