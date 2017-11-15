using UnityEngine;

public class PlayerBullet : BulletMovement {

    void OnEnable() {
        if (netherTracker) {
            this.isNether = netherTracker.isNether;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Boundaries")) 
        {
            ReturnToPool();
        }
    }

}
