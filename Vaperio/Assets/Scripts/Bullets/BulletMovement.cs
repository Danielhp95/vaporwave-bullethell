using UnityEngine;

public class BulletMovement : PooledObject {

    public float speed;
    public Vector3 direction;
    public int bulletDamage = 10;
	public bool isNether { get; protected set; }

    protected FlipWorld netherTracker;

    void Start() {
		this.netherTracker = GameObject.Find ("Foreground").GetComponent<FlipWorld>();
		this.isNether = netherTracker.isNether;
    }

    void Update() {
		if (!Pause.paused) {
			this.transform.Translate (this.direction.normalized * (speed * Time.deltaTime), Space.Self);
		}
    }
    
}
