using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralph : MonoBehaviour {
    
    public EnemyBullet bullet;
    private EnemyShoot shoot;
    private float timer;
    public float shootingTime = 0.8f;
	private Transform foreground;
    

	// Use this for initialization
	void Start () {
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
		foreground = GameObject.Find ("Foreground").transform;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > shootingTime){
            shootBullet();
            timer=0;
        }
		
	}
    
    void shootBullet() {
        Vector3 newBulletLocation = this.transform.position;
        BulletMovement newBullet = bullet.GetPooledInstance<BulletMovement>(newBulletLocation); 
        Vector3 offset = newBullet.direction * 0.8f;
        newBullet.transform.Translate(offset);	
    }
}
