using UnityEngine;

public class BulletMovement : PooledObject {

    public Vector3 speed;
    public Vector3 direction;
    public int bulletDamage = 10;

    void Start() {

    }

    void Update() {
        this.transform.position += Vector3.Project(this.speed, this.direction.normalized) * 
                                  Time.deltaTime;
        
    }
    
    
    void OnCollisionStay(Collision collision){
    
        if(collision.gameObject.layer != 9){
            Destroy(this.gameObject);
           
        }
        
    
     }
    
        

}
