using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private SpaceShipHealth spaceShipHealth; // Reference to the player's health.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public GameObject netherEnemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
	private GameObject foreground;
	public Vector3 minSpawnValues;
	public Vector3 maxSpawnValues;

    void Start ()
    {
        
        foreground = GameObject.Find("Foreground");
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

		GameObject toSpawn = shouldBeNether() ? netherEnemy : enemy;
		GameObject spawnedEnemy =  Instantiate (toSpawn, new Vector3(0f, 0f, 0f), foreground.transform.rotation, foreground.transform);
		spawnedEnemy.transform.Rotate (0, 180, 0);
		spawnedEnemy.transform.Translate (generatePoint ());
		spawnedEnemy.transform.Translate (new Vector3(0f, 0f, -4f), Space.World);
    }

	private bool shouldBeNether() {
		return Random.Range (0, 2) == 1;
	}
    
    private Vector3 generatePoint(){
        float x = (float) Random.Range (minSpawnValues.x, maxSpawnValues.x) ;
		float y = (float) Random.Range (minSpawnValues.y, maxSpawnValues.y) ;
		float z = (float) Random.Range (minSpawnValues.z, maxSpawnValues.z) ;
		return new Vector3 (x, y, z); 	
    }
}