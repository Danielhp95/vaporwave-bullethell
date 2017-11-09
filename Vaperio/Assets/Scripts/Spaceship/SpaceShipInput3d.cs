using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput3d : MonoBehaviour {
    
    private Rigidbody spaceshipBody;
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
        this.pastInputs = new Vector2[lagDuration];
        this.lagCount = lagDuration-1;
        for(int i=0; i < lagDuration; i = i+1){
			pastInputs[i]= new Vector2(0f, 0f);
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

    void handleShooting() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootBullet();
        }
    }
    
    void shootBullet() {
        
    }
    
}