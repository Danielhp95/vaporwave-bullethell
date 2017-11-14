using UnityEngine;

public class SpaceShipShooting : MonoBehaviour {

    public BulletMovement bullet;
	private Transform foreground;
	public float reloadTime = 0.5f;
	private float timeToShoot = 0f;
    public AudioClip shootSound;
    private AudioSource source;

	void Start() {
		foreground = GameObject.Find ("Foreground").transform;
        source = GetComponent<AudioSource>();
    }

    void Update(){
		reduceTimeToShoot ();
        handleShooting();
    }

	private void reduceTimeToShoot() {
		if (timeToShoot > 0f) {
			timeToShoot = Mathf.Max (timeToShoot - Time.deltaTime, 0f);
		}
	}

    private void handleShooting() {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.J))
			&& timeToShoot == 0) {
            shootBullet();
			timeToShoot = reloadTime;
        }
    }
    
    void shootBullet() {
        source.PlayOneShot(shootSound,1);
        Vector3 offset = getOffset();
        Vector3 newBulletLocation = this.transform.position;
        BulletMovement bulletMovement = bullet.GetPooledInstance<BulletMovement>(newBulletLocation); 
        bulletMovement.transform.Translate(offset);
    }
    
	private Vector3 getOffset() {
		return new Vector3(0.8f,0f,0f);
	}
}
