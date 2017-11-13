using UnityEngine;

public class PlayerBullet : BulletMovement {

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Boundaries")) 
        {
            ReturnToPool();
        }
    }

}
