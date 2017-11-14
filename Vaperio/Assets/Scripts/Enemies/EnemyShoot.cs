using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    public GameObject bullet;
    
    public void shootBullet() {
        Vector3 offset = new Vector3(0f,0f,0f); 
        Instantiate(bullet, this.transform.position + offset, Quaternion.identity);
    }
}
