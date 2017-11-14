using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralph : MonoBehaviour {
    
    public GameObject bullet;
    private EnemyShoot shoot;
    private float timer;
    public float shootingTime = 0.8f;
	private Transform foreground;
    public AudioClip shootSound;
    private AudioSource source;
    

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
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
        Vector3 offset = new Vector3(0.8f,0f,0f);
        GameObject newBullet = Instantiate(bullet, this.transform.position, foreground.rotation, foreground);
        newBullet.transform.Translate(offset);	
        float vol = Random.Range (0.8f, 1.0f);
        source.pitch= (Random.Range(0.8f,1.2f));
        source.PlayOneShot(shootSound,vol);
    }
}
