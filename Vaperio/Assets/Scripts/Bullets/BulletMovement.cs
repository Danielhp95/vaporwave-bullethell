using UnityEngine;

// Turn into Bullet Object with its own bullet prefab as a public variable?
public class BulletMovement : PooledObject {

    public float speed;
    public Vector3 direction;
    public int bulletDamage = 10;
	public bool isNether { get; private set; }

    public GameObject bulletPrefab;

    void Start() {
		FlipWorld netherTracker = GameObject.Find ("Foreground").GetComponent<FlipWorld>();
		isNether = netherTracker.isNether;
    }

    void Update() {
		if (!Pause.paused) {
			this.transform.Translate (
				direction.normalized * (speed * Time.deltaTime),
				Space.Self
			);
		}
    }
    
    void OnCollisionStay(Collision collision){
        if(collision.gameObject.layer != 9) {
            Destroy(this.gameObject);
        }
     }
    
        

}
