using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marge : MonoBehaviour {
    private enum Behaviour {APPROACHING, WAITFORSPIKE, SPIKE, RETURN};
    private Behaviour currentBehaviour;
    private GameObject player;  
    public float distanceThreshold = 0.1f;
    public float waitThreshold = 0.1f;
    public float spikeThreshold = 0.1f;
    private float flip =1.0f;
    private int wobbleWait= 0;
    private int wobbleThreshold = 3;
    private Vector3 waitPos;
    private Vector3 waitTargetPos;
    private Vector3 spikeInitPos;
    public AudioClip homieSound;
    private AudioSource homie;
    private bool spiking= false;
    
    
    

	// Use this for initialization
	void Start () {
        currentBehaviour = Behaviour.APPROACHING;
        player = GameObject.Find("spaceship3D");
        wobbleThreshold = Random.Range(2,5);
        homie = GetComponent<AudioSource>();
 
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleBehaviour();
		
	}
    
    private void HandleBehaviour(){
        switch(currentBehaviour){
            case Behaviour.APPROACHING:
                ApproachPlayer();
                MoveToNextStateApproaching();
                break;
            case Behaviour.WAITFORSPIKE:
                Wait();
                MoveToNextStateWait();
                break;
            case Behaviour.SPIKE:
                SpikeAttack();
                
                break;
            case Behaviour.RETURN:
                Return();
                
                break;
        }

    }
    
    private void ApproachPlayer(){
        float newX = Vector3.MoveTowards(transform.position, player.transform.position,0.3f).x;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    

    private void MoveToNextStateApproaching(){
        float distance = Mathf.Abs(transform.position.x - player.transform.position.x);
        if(distance < distanceThreshold){
            waitPos = transform.position;
            waitTargetPos = transform.position + new Vector3((transform.position.x + (Random.Range(0.5f,1f))), 0,0);
            currentBehaviour = Behaviour.WAITFORSPIKE;
        } 
        
    }
    
    private void ChangeToApproaching() {
        currentBehaviour = Behaviour.APPROACHING;
    } 
    
    private void Wait(){
        Wobble();
    }       
    
    
    private void  Wobble() {
        
        float waitDistance = Mathf.Abs(transform.position.x - waitTargetPos.x);
        transform.position = Vector3.MoveTowards(transform.position, waitTargetPos,0.03f);
        if(waitDistance<waitThreshold) {
            waitTargetPos = waitPos + new Vector3(((Random.Range(flip*0.2f,flip*0.7f))), 0,0);
            flip *= -1;
            wobbleWait += 1;
            
        }
    
    }
    
    
    private void MoveToNextStateWait(){
        if (wobbleWait == wobbleThreshold) {
            wobbleWait = 0;
            spikeInitPos = transform.position;
            currentBehaviour = Behaviour.SPIKE;
            
        }
    }
    
    private void SpikeAttack(){
        if(!spiking){
        Vector3 spikeTargetPos = spikeInitPos + new Vector3(0,7f,0);
        transform.position = Vector3.MoveTowards(transform.position, spikeTargetPos,0.4f);
        //homie.PlayOneShot(homieSound,1);
        float spikeDistance = Mathf.Abs(transform.position.y - spikeTargetPos.y);
            if (spikeDistance<spikeThreshold){
                spiking = true;
                StartCoroutine(waitForSpikeDown());
            }
        }
    }
    
    private void Return(){
        transform.position = Vector3.MoveTowards(transform.position, waitPos,0.2f);
        //homie.PlayOneShot(homieSound,1);
        float returnDistance = Mathf.Abs(transform.position.y - waitPos.y);
        if (returnDistance<distanceThreshold){
            MoveToNextStateReturn();
        }
    }
    
    
    IEnumerator waitForSpikeDown() {
        yield return new WaitForSeconds(2);
        MoveToNextStateSpike();
        
        
    }
    
    private void MoveToNextStateSpike(){
        spiking = false;
        currentBehaviour = Behaviour.RETURN;      
        
    }
    
    private void MoveToNextStateReturn(){
        currentBehaviour = Behaviour.APPROACHING;      
        
    }
} 


