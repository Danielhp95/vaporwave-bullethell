using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput3d : MonoBehaviour {
    
    private Rigidbody spaceshipBody;
    public float VerticalSpeed = 10.0f, HorizontalSpeed=10.0f, DepthSpeed=10.0f;
    public int lagDur = 10;
    private int lagInd = 0, lagCount;
    private float[] hInputLag;
	public float verticalForce = 7f;
	public float horizontalForce = 7f;
	public float depthForce = 10f;
    public int lagDuration = 20;
	private int lagIndex = 0, lagCount;
    private Vector2[] pastInputs;
	private float maxSpeed = 2f;
    

	// Use this for initialization
	void Start () {
		this.spaceshipBody = GetComponent<Rigidbody>();
        this.hInputLag = new float[lagDur];
        this.lagCount = lagDur-1;
        for(int i=0; i < lagDur; i = i+1){
            hInputLag[i]=0.0f;
        }
        
        this.pastInputs = new Vector2[lagDuration];
        this.lagCount = lagDuration-1;
        for(int i=0; i < lagDuration; i = i+1){
			pastInputs[i]= new Vector2(0f, 0f);
        }        
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
        lagInd = lagInd + 1;
        if(lagInd == lagCount){lagInd = 0;} 
        lagIndex = lagIndex + 1;
        if(lagIndex == lagCount){lagIndex = 0;}
	}


    void handleMovement() {
        Vector3 speedH = new Vector3(0,HorizontalSpeed,0);
        Vector3 speedV = new Vector3(-1.0f*(VerticalSpeed),0,0);
        Vector3 speedZ = new Vector3(0,0,DepthSpeed);
        Vector3 translation;
        hInputLag[lagInd]=0.0f; //comment for crazy effect
		setDrag ();
		getCurrentInput ();

		applyPastInputs ();

		applyMaxSpeed ();
	}

	private void getCurrentInput() {
		pastInputs[lagIndex] = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis ("Vertical"));
	}

	private void applyPastInputs() {
		Vector2 pastInput = pastInputs[lagCount-lagIndex];

		Vector3 force = new Vector3 (pastInput.x * horizontalForce, pastInput.y * verticalForce * -1f);

		force *= Time.deltaTime;

		spaceshipBody.AddRelativeForce(force, ForceMode.Impulse);

	}

	private void setDrag () {
		float speed = spaceshipBody.velocity.magnitude;
		spaceshipBody.drag = Mathf.Max (Mathf.Pow (speed, 3), 0.5f);
	}

	private void applyMaxSpeed() {
		Vector3 newVelocity = spaceshipBody.velocity;
		if (newVelocity.magnitude > maxSpeed) {
			spaceshipBody.velocity = newVelocity.normalized * maxSpeed;
		}
	}
    
        if(Input.GetAxis("Horizontal") != 0){
            hInputLag[lagInd]=Input.GetAxis("Horizontal");
            translation = hInputLag[lagCount-lagInd] * speedH;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
        
        if(Input.GetAxis("Vertical") != 0){
            hInputLag[lagInd]=Input.GetAxis("Vertical");
            translation = hInputLag[lagCount-lagInd] * speedV;
            translation *= Time.deltaTime;
            spaceshipBody.AddRelativeForce(translation, ForceMode.Impulse);
        }
    }
}
