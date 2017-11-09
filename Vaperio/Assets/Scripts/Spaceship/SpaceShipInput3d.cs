using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput3d : MonoBehaviour {
    
    private Rigidbody spaceshipBody;
	public float VerticalAcceleration = 7f;
	public float HorizontalAcceleration = 7f;
	public float DepthAcceleration = 10f;
    public int lagDuration = 10;
	private int lagIndex = 0, lagCount;
    private float[] hInputLag;
	private float maxSpeed = 2f;
    

	// Use this for initialization
	void Start () {
		this.spaceshipBody = GetComponent<Rigidbody>();
        this.hInputLag = new float[lagDuration];
        this.lagCount = lagDuration-1;
        for(int i=0; i < lagDuration; i = i+1){
            hInputLag[i]=0.0f;
        }        
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
        handleShooting();
        lagIndex = lagIndex + 1;
        if(lagIndex == lagCount){lagIndex = 0;} 
		
	}


    void handleMovement() {
		Vector3 speedH = new Vector3(0, -1f * HorizontalAcceleration, 0);
        Vector3 speedV = new Vector3(VerticalAcceleration, 0, 0);
        Vector3 speedZ = new Vector3(0, 0, DepthAcceleration);
        Vector3 translation;
        hInputLag[lagIndex]=0.0f; //comment for crazy effect
    
        if(Input.GetAxis("Horizontal") != 0){
            hInputLag[lagIndex]=Input.GetAxis("Horizontal");
            translation = hInputLag[lagCount-lagIndex] * speedV;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
        
        if(Input.GetAxis("Vertical") != 0){
            hInputLag[lagIndex]=Input.GetAxis("Vertical");
            translation = hInputLag[lagCount-lagIndex] * speedH;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
		applyMaxSpeed ();
    }

	private void applyMaxSpeed() {
		Vector3 newVelocity = spaceshipBody.velocity;
		if (newVelocity.magnitude > maxSpeed) {
			spaceshipBody.velocity = newVelocity.normalized * maxSpeed;
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