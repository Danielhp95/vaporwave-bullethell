using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralph : MonoBehaviour {
    
    public EnemyBullet bullet;
    private EnemyShoot shoot;
    private float timer;
    public float shootingTime = 0.8f;
	private Transform foreground;
    
	void Start () {
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
		foreground = GameObject.Find ("Foreground").transform;
	}

	void Update () {
		if (!Pause.paused) {
			timer += Time.deltaTime;
			if (timer > shootingTime) {
				shootBullet ();
				timer = 0;
			}
		}
	}
    
    void shootBullet() {
        Vector3 newBulletLocation = this.transform.position;
        BulletMovement newBullet = bullet.GetPooledInstance<BulletMovement>(newBulletLocation); 
        Vector3 offset = newBullet.direction * 0.8f;
        newBullet.transform.Translate(offset);	
    }
}
