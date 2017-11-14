using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput : MonoBehaviour {

    private Rigidbody2D spaceshipBody;

    
	void Start () {
        this.spaceshipBody = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if (!Pause.paused) {
			handleMovement ();
			handleShooting ();
		}
	}
    
    void handleMovement() {
        Vector2 speedH = new Vector2(0,10);
        Vector2 speedV = new Vector2(-10,0);
        Vector2 translation;
        
		//Translation is an special case, only occurs if the player wants to go up!
		if(Input.GetKey(KeyCode.D)){
			translation = Input.GetAxis("Horizontal") * speedH;
			translation *= Time.deltaTime;
			spaceshipBody.AddRelativeForce(translation, ForceMode2D.Impulse);
		}
        if(Input.GetKey(KeyCode.A)){
			translation = Input.GetAxis("Horizontal") * speedH;
			translation *= Time.deltaTime;
			spaceshipBody.AddRelativeForce(translation, ForceMode2D.Impulse);
		}
        
        if(Input.GetKey(KeyCode.S)){
			translation = Input.GetAxis("Vertical") * speedV;
			translation *= Time.deltaTime;
			spaceshipBody.AddRelativeForce(translation, ForceMode2D.Impulse);
		}
        
        if(Input.GetKey(KeyCode.W)){
			translation = Input.GetAxis("Vertical") * speedV;
			translation *= Time.deltaTime;
			spaceshipBody.AddRelativeForce(translation, ForceMode2D.Impulse);
		}
        /*
        else if( Input.GetAxis("Horizontal") > 0 ){
			Vector2 translationH = Input.GetAxis("Horizontal") * speed;
			translation *= Time.deltaTime;
			spaceshipBody.AddRelativeForce(translationH, ForceMode2D.Impulse);
		}
        */
        
    }
    
    void handleShooting() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootBullet();
        }
    }
    
    void shootBullet() {
        
    }
    
    
}
