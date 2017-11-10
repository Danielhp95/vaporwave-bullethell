using UnityEngine;

public class SpaceShipShooting : MonoBehaviour {

    public BulletMovement bullet;
	private Transform foreground;
	private int defaultYRotation = 180;

	void Start() {
		foreground = GameObject.Find ("Foreground").transform;
    }

    void Update(){
        handleShooting();
    }

    private void handleShooting() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootBullet();
        }
    }
    
    void shootBullet() {
        Vector3 offset = getOffset();
        Vector3 newBulletLocation = this.transform.position + offset;
        BulletMovement bm = bullet.GetPooledInstance<BulletMovement>(newBulletLocation, Quaternion.identity); 
        bm.transform.Translate(offset);
    }
    
	private Vector3 getOffset() {
		return new Vector3(-0.8f,0f,0f);
	}
}
