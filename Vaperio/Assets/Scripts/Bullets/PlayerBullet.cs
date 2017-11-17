using UnityEngine;

public class PlayerBullet : BulletMovement {
	Transform childTransform;
	float netherDirection = -0.0001f;
	float normalDirection = 0.0001f;

	void Start() {
		PerformNetherCheck ();
	}

    void OnEnable() {
		PerformNetherCheck ();
    }

	private void PerformNetherCheck() {
		if (!childTransform)
			GetChildTransform ();
		ReturnSpriteToDefaultPosition ();
		SetNetherTracker ();
		FlipSprites ();
	}

	private void GetChildTransform() {
		childTransform = gameObject.gameObject.GetComponentsInChildren<Transform> ()[1];
	}

	private void ReturnSpriteToDefaultPosition () {
		if (isNether) {
			childTransform.Translate (0f, 0f, normalDirection);
		} else {
			childTransform.Translate (0f, 0f, netherDirection);
		}
	}

	private void SetNetherTracker() {
		if (netherTracker) {
			this.isNether = netherTracker.isNether;
		} else {
			this.netherTracker = GameObject.Find ("Foreground").GetComponent<FlipWorld>();
			this.isNether = netherTracker.isNether;
		}
	}

	private void FlipSprites() {
		if (isNether) {
			FlipForNether ();
		} else {
			FlipForNormal ();
		}
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Boundaries")) 
        {
            ReturnToPool();
        }
    }

	private void FlipForNether() {
		childTransform.Translate (0f, 0f, netherDirection);
	}

	private void FlipForNormal() {
		childTransform.Translate (0f, 0f, normalDirection);
	}
}
