using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralph : MonoBehaviour {
    
    public EnemyBullet bullet;
    private EnemyShoot shoot;
    private int shotsFired;
    private int maximumNumberOfShots;

    public Vector3 targetPosition;
    public Vector3 directionToTargetPosition;
    private float speed = 2.5f;

    private float timer;
    public float shootingTime = 0.8f;

	private Transform foreground;

    public AudioClip shootSound;
    private AudioSource source;

    public enum BehaviourState { APPROACHING, SHOOTING, LEAVING };
    private BehaviourState currentBehaviour;

    private FlipWorld netherTracker;

    
	void Start () {
        source = GetComponent<AudioSource>();
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
		foreground = GameObject.Find ("Foreground").transform;

		netherTracker = GameObject.Find ("Foreground").GetComponent<FlipWorld>();
	}

    public void InitializeRalph(Vector3 initialPosition, Vector3 targetPosition, int maximumNumberOfShots,
                                BehaviourState initialBehaviour = BehaviourState.APPROACHING)
    {
        if (netherTracker == null) {
            netherTracker = GameObject.Find ("Foreground").GetComponent<FlipWorld>();
        }
        this.targetPosition = targetPosition;
        this.targetPosition.z = -4f; //TODO: Change this

        this.transform.Translate(initialPosition);

        this.directionToTargetPosition = (targetPosition - this.transform.position).normalized;
        if (netherTracker.isNether) { this.directionToTargetPosition.x *= -1; }

        this.maximumNumberOfShots = maximumNumberOfShots;
        currentBehaviour = initialBehaviour;
    }

	void Update () {
        if (!Pause.paused) {
            HandleBehaviour();
		}
	}

    void HandleBehaviour() {
       switch (currentBehaviour) {
            case BehaviourState.APPROACHING: HandleApproaching(); break;
            case BehaviourState.SHOOTING: HandleShooting(); break;
            case BehaviourState.LEAVING: HandleLeaving(); break;
       }
       CheckForBehaviourChange();
    }


    void HandleApproaching() {
        this.transform.Translate((this.directionToTargetPosition * speed) * Time.deltaTime, Space.Self);
    }

    void HandleShooting() {
        timer += Time.deltaTime;
        if (timer > shootingTime) {
            shootBullet ();
            timer = 0;
            this.shotsFired++;
        }
    }

    void HandleLeaving() {
        Vector3 leftDirection = new Vector3(-1f,0f,0f);
        this.transform.Translate((leftDirection * speed) * Time.deltaTime, Space.Self);
    }

    void CheckForBehaviourChange() {
       switch (currentBehaviour) {
            case BehaviourState.APPROACHING:
                if (HasTargetPositionBeenReached()) {
                    this.currentBehaviour = BehaviourState.SHOOTING;
                }
                break;
            case BehaviourState.SHOOTING:
                if (HasMaximumShootsBeenFired()) {
                    this.currentBehaviour = BehaviourState.LEAVING;
                }
                break;
            case BehaviourState.LEAVING:
                // TODO: check that Ralph is actually destroyed
                break;
       }
    }

    private bool HasTargetPositionBeenReached() {
        float threshold = 0.1f;
        Vector3 target = CalculateTargetAccountingForNetherPosition();

        float distanceToTarget = Vector3.Distance(this.transform.position, target);
        return distanceToTarget < threshold;
    }

    private Vector3 CalculateTargetAccountingForNetherPosition() {
        Vector3 realTarget = this.targetPosition;
        realTarget.x = netherTracker.isNether ? -1 * this.targetPosition.x : this.targetPosition.x;
        return realTarget;
    }

    private bool HasMaximumShootsBeenFired() {
        return this.shotsFired == this.maximumNumberOfShots;
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
