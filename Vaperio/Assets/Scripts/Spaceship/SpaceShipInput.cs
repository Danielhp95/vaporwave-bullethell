using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput : MonoBehaviour {

    private Rigidbody2D spaceshipBody;

    
	// Use this for initialization
	void Start () {
        this.spaceshipBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
        handleShooting();
	}
    
    void handleMovement() {
        //Deal with rotation first
        float rotationSpeed = 200f;
        Vector2 speed = new Vector2( 0f, 10.0F);
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		rotation *= Time.deltaTime;
		transform.Rotate(0, 0 , -rotation);
        
		//Translation is an special case, only occurs if the player wants to go up!
		if( Input.GetAxis("Vertical") > 0 ){
			Vector2 translation = Input.GetAxis("Vertical") * speed;
			translation *= Time.deltaTime;
			spaceshipBody.AddRelativeForce(translation, ForceMode2D.Impulse);
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
