using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput3d : MonoBehaviour {
    
    private Rigidbody spaceshipBody;
    public float VerticalSpeed = 10.0f, HorizontalSpeed=10.0f, DepthSpeed=10.0f;
    public int lagDur = 10;
    private int lagInd = 0, lagCount;
    private float[] hInputLag;
    

	// Use this for initialization
	void Start () {
		this.spaceshipBody = GetComponent<Rigidbody>();
        this.hInputLag = new float[lagDur];
        this.lagCount = lagDur-1;
        for(int i=0; i < lagDur; i = i+1){
            hInputLag[i]=0.0f;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
        handleShooting();
        lagInd = lagInd + 1;
        if(lagInd == lagCount){lagInd = 0;} 
		
	}


    void handleMovement() {
        Vector3 speedH = new Vector3(0, HorizontalSpeed, 0);
        Vector3 speedV = new Vector3((VerticalSpeed), 0, 0);
        Vector3 speedZ = new Vector3(0, 0, DepthSpeed);
        Vector3 translation;
        hInputLag[lagInd]=0.0f; //comment for crazy effect
    
        if(Input.GetAxis("Horizontal") != 0){
            hInputLag[lagInd]=Input.GetAxis("Horizontal");
            translation = hInputLag[lagCount-lagInd] * speedV;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
        
        if(Input.GetAxis("Vertical") != 0){
            hInputLag[lagInd]=Input.GetAxis("Vertical");
            translation = hInputLag[lagCount-lagInd] * speedH;
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