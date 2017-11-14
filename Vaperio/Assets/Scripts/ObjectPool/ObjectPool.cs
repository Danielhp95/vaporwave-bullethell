using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    PooledObject prefab;
    List<PooledObject> availableObjects = new List<PooledObject>();

	void Start () {
		Transform foreground = GameObject.Find ("Foreground").transform;
		this.transform.SetParent (foreground, true);
        AccountForNetherWorldOrientation(foreground);
	}

    private void AccountForNetherWorldOrientation(Transform foreground) {
        if (foreground.GetComponent<FlipWorld>().isNether) {
           this.transform.Rotate(0f,180f,0f); 
        }
    }

    public PooledObject GetObject(Vector3 position) {
        PooledObject pooledObject; 
        int lastAvailableIndex = availableObjects.Count - 1;
        if (lastAvailableIndex >= 0) {
			pooledObject = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            pooledObject.gameObject.SetActive(true);
        } else {
            pooledObject = Instantiate<PooledObject>(prefab);
            pooledObject.transform.SetParent(transform, true);
            pooledObject.Pool = this;
        }
        pooledObject = SetTransformInformation(pooledObject, position);
        return pooledObject;
    }

    private PooledObject SetTransformInformation(PooledObject obj, Vector3 position) {
        obj.transform.position = position;
		obj.transform.rotation = obj.transform.parent.rotation;
        return obj;
    }

    public static ObjectPool GetPool(PooledObject prefab) {
        GameObject obj = new GameObject(prefab.name + "Pool");
        ObjectPool pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }

    public void AddObject(PooledObject obj) {
        obj.gameObject.SetActive(false);
        availableObjects.Add(obj);
    }
}
