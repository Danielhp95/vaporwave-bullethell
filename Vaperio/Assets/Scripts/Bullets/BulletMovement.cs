using UnityEngine;

// Turn into Bullet Object with its own bullet prefab as a public variable?
public class BulletMovement : PooledObject {

    public Vector3 speed;
    public Vector3 direction;

    public GameObject bulletPrefab;
    public int framesToLive = 10;

    void Start() {

    }

    void Update() {
        if (framesToLive <= 0 ) {
            framesToLive = 10;
            this.ReturnToPool();
        } else {
            framesToLive -= 1;
        }
        this.transform.position += Vector3.Project(this.speed, this.direction.normalized) * 
                                  Time.deltaTime;
    }
}
