using UnityEngine;

public class EnemyBullet : BulletMovement {

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Boundaries") ||
            collider.gameObject.layer == LayerMask.NameToLayer("Spaceship")) 
        {
            ReturnToPool();
        }
    }

}
