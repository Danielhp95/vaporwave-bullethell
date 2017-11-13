using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
	public float speed = 0.005f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 translation = new Vector2 (speed * Time.deltaTime, 0f);
		var material = GetComponent<Renderer>().material;
		material.mainTextureOffset =  translation + material.mainTextureOffset;
	}
}
