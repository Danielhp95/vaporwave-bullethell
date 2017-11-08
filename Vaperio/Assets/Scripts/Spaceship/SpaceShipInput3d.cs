using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput3d : MonoBehaviour {
    
    private Rigidbody spaceshipBody;
    public float VerticalSpeed = 10.0f, HorizontalSpeed=10.0f, DepthSpeed=10.0f;

	// Use this for initialization
	void Start () {
		this.spaceshipBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
        handleShooting();
		
	}


    void handleMovement() {
        Vector3 speedH = new Vector3(0,HorizontalSpeed,0);
        Vector3 speedV = new Vector3(-1.0f*(VerticalSpeed),0,0);
        Vector3 speedZ = new Vector3(0,0,DepthSpeed);
        Vector3 translation;
    
        if(Input.GetAxis("Horizontal") != 0){
            translation = Input.GetAxis("Horizontal") * speedH;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
        
        if(Input.GetAxis("Vertical") != 0){
            translation = Input.GetAxis("Vertical") * speedV;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
    }

    void handleShooting() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootBullet();
        }
    }
    
    void shootBullet() {
        
    }
    
}