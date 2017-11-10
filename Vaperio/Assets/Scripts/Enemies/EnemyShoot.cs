using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    public GameObject bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.Q)) {
            shootBullet();
        }
		*/
	}
    
    public void shootBullet() {
        Vector3 offset = new Vector3(0f,0f,0f); 
        Instantiate(bullet, this.transform.position + offset, Quaternion.identity);
    }
}
