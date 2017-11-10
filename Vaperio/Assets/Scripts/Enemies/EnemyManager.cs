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

        GameObject spawnedEnemy =  Instantiate (enemy, generatePoint(), foregroundTransform.rotation, foregroundTransform);
        spawnedEnemy.transform.Rotate(0,180,0);
    }
    
    private Vector3 generatePoint(){
        float x = (float) Random.Range (minSpawnValues.x, maxSpawnValues.x) ;
		float y = (float) Random.Range (minSpawnValues.y, maxSpawnValues.y) ;
		float z = (float) Random.Range (minSpawnValues.z, maxSpawnValues.z) ;
		return new Vector3 (x, y, z); 	
    }
}