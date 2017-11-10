using UnityEngine;

// Turn into Bullet Object with its own bullet prefab as a public variable?
public class BulletMovement : PooledObject {

    public Vector3 speed;
    public Vector3 direction;
    public int bulletDamage = 10;

    public GameObject bulletPrefab;

    void Start() {

    }

    void Update() {
		this.transform.Translate(
			Vector3.Project(this.speed, this.direction.normalized) *  Time.deltaTime,
			Space.Self
		);
        
    }
    
    
    void OnCollisionStay(Collision collision){
        //print(collision);
        //print(collision.gameObject.layer);
        if(collision.gameObject.layer != 9){
            Destroy(this.gameObject);
        }
     }
    
        

}
