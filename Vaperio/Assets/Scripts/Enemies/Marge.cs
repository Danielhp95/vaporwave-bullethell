using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marge : MonoBehaviour {
    private enum Behaviour {APPROACHING, WAITFORSPIKE, SPIKE, RETURN};
    private Behaviour currentBehaviour;
    private GameObject player;  
    public float distanceThreshold = 0.1f;
    private float flip = 1.0f;
	private int wobbleCount = 0;
    private int wobbleNumber = 3;
	private Vector3 waitPosition;
	private Vector3 wobbleTargetPosition;
    public AudioClip homieSound;
    public AudioClip reverseHomieSound;
    private AudioSource homie;
    private bool spiking = false;
	public float spikeSpeed = 8f;
	public float returnSpeed = 3f;
    protected FlipWorld netherTracker;

    // Use this for initialization
    void Start ()
    {
        this.netherTracker = GameObject.Find("Foreground").GetComponent<FlipWorld>();
        currentBehaviour = Behaviour.APPROACHING;
        player = GameObject.Find("spaceship3D");
        wobbleNumber = Random.Range(2,5);
        homie = GetComponentInChildren<AudioSource>();
 
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Pause.paused) {
			HandleBehaviour ();
		}
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
            waitPosition = transform.position;
            wobbleTargetPosition = transform.position + new Vector3((transform.position.x + (Random.Range(0.5f,1f))), 0,0);
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
        
        float waitDistance = Mathf.Abs(transform.position.x - wobbleTargetPosition.x);
        transform.position = Vector3.MoveTowards(transform.position, wobbleTargetPosition,0.03f);
        if(waitDistance < distanceThreshold) {
            wobbleTargetPosition = waitPosition + new Vector3(((Random.Range(flip*0.2f,flip*0.7f))), 0,0);
            flip *= -1;
            wobbleCount  += 1;
            
        }
    
    }
    
    
    private void MoveToNextStateWait(){
        if (wobbleCount  == wobbleNumber) {
            wobbleCount  = 0;
			waitPosition = transform.position;
            currentBehaviour = Behaviour.SPIKE;
			spiking = true;
            playHomie();
        }
    }
    
    private void SpikeAttack(){
        if(spiking) {
			Vector3 spikeTargetPos = new Vector3(transform.position.x, waitPosition.y + 7f, transform.position.z);
			transform.Translate(new Vector3(0f, spikeSpeed * Time.deltaTime, 0f));
            if (transform.position.y  >= spikeTargetPos.y){
                spiking = false;
                StartCoroutine(waitForSpikeDown());
            }
        }
    }
    
    private void Return(){
		Vector3 toReturnTo = new Vector3 (transform.position.x, waitPosition.y, transform.position.z);
        transform.Translate(new Vector3(0f, -returnSpeed * Time.deltaTime, 0f));
        float returnDistance = Mathf.Abs(transform.position.y - waitPosition.y);
        if (returnDistance<distanceThreshold){
            MoveToNextStateReturn();
        }
    }
    

    private void playHomie()
    {
        float vol = Random.Range(0.4f, 0.6f);
        homie.pitch = (Random.Range(0.9f, 1.1f));
        AudioClip clip = netherTracker.isNether ? reverseHomieSound : homieSound;
        homie.PlayOneShot(clip, vol);
    }
    
    IEnumerator waitForSpikeDown() {
        yield return new WaitForSeconds(2);
        MoveToNextStateSpike();
    }
    
    private void MoveToNextStateSpike(){
        currentBehaviour = Behaviour.RETURN;
    }
    
    private void MoveToNextStateReturn(){
        currentBehaviour = Behaviour.APPROACHING;      
    }
} 
