using UnityEngine;

public class BulletMovement : PooledObject {

    public Vector3 speed;
    public Vector3 direction;
    public int bulletDamage = 10;
	public bool isNether { get; private set; }

    void Start() {
		FlipWorld netherTracker = GameObject.Find ("Foreground").GetComponent<FlipWorld>();
		isNether = netherTracker.isNether;
    }

    void Update() {
		this.transform.Translate(
			Vector3.Project(this.speed, this.direction.normalized) *  Time.deltaTime,
			Space.Self
		);
        
    }
    
    void OnaTriggerEnter(Collider collider) {
        print(collider);
    }

    void OnCollisionStay(Collision collision){
        int spaceShipLayer = 9;
        if(collision.gameObject.layer != spaceShipLayer) {
            Destroy(this.gameObject);
        }
     }
    
        

}
