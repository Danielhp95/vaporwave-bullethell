using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralph : MonoBehaviour {
    
    public EnemyBullet bullet;
    private EnemyShoot shoot;
    private float timer;
    public float shootingTime = 0.8f;
	private Transform foreground;
    public AudioClip shootSound;
    private AudioSource source;


    private enum BehaviourState { APPROACHING, SHOOTING };
    private BehaviourState currentBehaviour;
    
	void Start () {
        source = GetComponent<AudioSource>();
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
		foreground = GameObject.Find ("Foreground").transform;

        currentBehaviour = BehaviourState.SHOOTING;
	}

	void Update () {
        HandleBehaviour();
	}

    void HandleBehaviour() {
       switch (currentBehaviour) {
            case BehaviourState.APPROACHING: HandleApproaching(); break;
            case BehaviourState.SHOOTING: HandleShooting(); break;
       }
    }


    void HandleApproaching() {

    }

    void HandleShooting() {
		if (!Pause.paused) {
			timer += Time.deltaTime;
			if (timer > shootingTime) {
				shootBullet ();
				timer = 0;
			}
		}
    }
    
    void shootBullet() {
        Vector3 newBulletLocation = this.transform.position;
        BulletMovement newBullet = bullet.GetPooledInstance<BulletMovement>(newBulletLocation); 
        Vector3 offset = newBullet.direction * 0.8f;
        newBullet.transform.Translate(offset);	
        float vol = Random.Range (0.8f, 1.0f);
        source.pitch= (Random.Range(0.8f,1.2f));
        source.PlayOneShot(shootSound,vol);
    }
}
