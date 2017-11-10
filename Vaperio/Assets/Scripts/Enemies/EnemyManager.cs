using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private SpaceShipHealth spaceShipHealth; // Reference to the player's health.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    private Transform foregroundTransform;
    public Vector3 minSpawnValues;
    public Vector3 maxSpawnValues;

    void Start ()
    {
        
        foregroundTransform = GameObject.Find("Foreground").transform;
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        /* If the player has no health left...
        if(spaceShipHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }
        */

        Instantiate (enemy, generatePoint(minSpawnValues,maxSpawnValues), foregroundTransform.rotation, foregroundTransform);
    }
    
    private Vector3 generatePoint(Vector3 min,Vector3 max){
        float x = (float) Random.Range (min.x, max.x) * 0.01f;
		float y = (float) Random.Range (min.y, max.y) * 0.01f;
		float z = (float) Random.Range (min.z, max.z) * 0.01f;
		return new Vector3 (x, y, z); 	
    }
}