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
        Instantiate(bullet, this.transform.position + offset, foreground.rotation, foreground);
    }
    
	private Vector3 getOffset() {
		Vector3 offset =  new Vector3(0.8f,0f,0f);
		if (!foreground.eulerAngles.y.Equals(defaultYRotation)) {
			offset.x = -offset.x;
		}
		return offset;
	}
}
