using UnityEngine;

public class PooledObject : MonoBehaviour {

    public ObjectPool Pool { get; set; }

    [System.NonSerialized]
    private ObjectPool poolInstanceForPrefab;

    protected void ReturnToPool () {
        if (Pool) {
            Pool.AddObject(this);
        } else {
            Destroy(gameObject);
        }
    }

    public T GetPooledInstance<T>(Vector3 position) where T: PooledObject {
        if (!poolInstanceForPrefab) {
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        }
        return (T)poolInstanceForPrefab.GetObject(position);
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("boundaries")) {
            ReturnToPool();
        }
    }

}
