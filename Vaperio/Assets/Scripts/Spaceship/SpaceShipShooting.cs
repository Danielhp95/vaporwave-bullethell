using UnityEngine;

public class SpaceShipShooting : MonoBehaviour {

    public GameObject bullet;
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
    
    private void shootBullet() {
		Vector3 offset = getOffset();
        GameObject newBullet = Instantiate(bullet, this.transform.position, foreground.rotation, foreground);
        newBullet.transform.Translate(offset);
    }
    
	private Vector3 getOffset() {
		return new Vector3(-0.8f,0f,0f);
	}
}
