using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput3d : MonoBehaviour {
    
    private Rigidbody spaceshipBody;
    public float VerticalSpeed = 10.0f, HorizontalSpeed=10.0f, DepthSpeed=10.0f;
    private float[] hInputLag;
	public float verticalForce = 7f;
	public float horizontalForce = 7f;
	public float depthForce = 10f;
    public int lagDuration = 20, lagDur=10, lagInd=0;
	private int lagIndex = 0, lagCount;
    private Vector2[] pastInputs;
    

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

	void Update () {
		if (!Pause.paused) {
			handleMovement ();
			lagInd = lagInd + 1;
			if (lagInd == lagCount) {
				lagInd = 0;
			} 
			lagIndex = lagIndex + 1;
			if (lagIndex == lagCount) {
				lagIndex = 0;
			}
		} else {
			StopShip ();
		}
	}


    void handleMovement() {
		setDrag ();
		getCurrentInput ();

		applyPastInputs ();
	}

	private void getCurrentInput() {
		pastInputs[lagIndex] = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis ("Vertical"));
	}

	private void applyPastInputs() {
		Vector2 pastInput = pastInputs[(lagIndex + 1) % lagDuration];

		Vector3 force = new Vector3 (pastInput.x * horizontalForce, pastInput.y * verticalForce * -1f);

		force *= Time.deltaTime;

		spaceshipBody.AddRelativeForce(force, ForceMode.Impulse);

	}

	    private void setDrag () {
		    float speed = spaceshipBody.velocity.magnitude;
		    spaceshipBody.drag = Mathf.Max (Mathf.Pow (speed, 2f), 0.5f);
	}   
    
	private void StopShip() {
		spaceshipBody.velocity = new Vector3 (0f, 0f, 0f);
	}
}
