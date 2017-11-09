using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    PooledObject prefab;
    List<PooledObject> availableObjects = new List<PooledObject>();

    public PooledObject GetObject(Vector3 position, Quaternion rotation) {
        PooledObject obj; 
        int lastAvailableIndex = availableObjects.Count - 1;
        if (lastAvailableIndex >= 0) {
            obj = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);
        } else {
            obj = Instantiate<PooledObject>(prefab);
            obj.transform.SetParent(transform, false);
            obj.Pool = this;
        }
        obj = SetTransformInformation(obj, position, rotation);
        return obj;
    }

    private PooledObject SetTransformInformation(PooledObject obj, Vector3 position, Quaternion rotation) {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
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
