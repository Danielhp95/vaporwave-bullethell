using UnityEngine;

// Turn into Bullet Object with its own bullet prefab as a public variable?
public class BulletMovement : PooledObject {

    public Vector3 speed;
    public Vector3 direction;

    public GameObject bulletPrefab;

    void Start() {

    }

    void Update() {
        this.transform.position += Vector3.Project(this.speed, this.direction.normalized) * 
                                  Time.deltaTime;
    }
}
