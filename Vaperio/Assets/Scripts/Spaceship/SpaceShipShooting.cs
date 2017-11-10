using UnityEngine;

public class SpaceShipShooting : MonoBehaviour {

    public GameObject bullet;
	private Transform foreground;

	void Start() {
		foreground = GameObject.Find ("Foreground").transform;
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
        Instantiate(bullet, this.transform.position + offset, foreground.rotation, foreground);
    }
    
}
