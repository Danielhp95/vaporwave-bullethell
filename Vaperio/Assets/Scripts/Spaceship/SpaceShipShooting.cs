using UnityEngine;

public class SpaceShipShooting : MonoBehaviour {

    public GameObject bullet;

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
        Instantiate(bullet, this.transform.position + offset, Quaternion.identity);
    }
    
}
