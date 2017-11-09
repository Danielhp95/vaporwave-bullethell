using UnityEngine;

public class SpaceShipShooting : MonoBehaviour {

    public BulletMovement bullet;

    void Start() {

    }

    void Update(){
        handleShooting();
    }

    void handleShooting() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootBullet();
        }
    }
    
    void shootBullet() {
        Vector3 offset = new Vector3(0.8f,0f,0f);
        Vector3 newBulletLocation = this.transform.position + offset;
        BulletMovement bm = bullet.GetPooledInstance<BulletMovement>(newBulletLocation, Quaternion.identity); 
    }
    
}
