using UnityEngine;

public class BulletMovement : PooledObject {

    public Vector3 speed;
    public Vector3 direction;

    void Start() {

    }

    void Update() {
        this.transform.position += Vector3.Project(this.speed, this.direction.normalized) * 
                                  Time.deltaTime;
    }
}
