using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public GameObject netherEnemy;                // The enemy prefab to be spawned.
    public float spawnTime = 1f;            // How long between each spawn.
	private GameObject foreground;
	private float timeSinceLastSpawn = 0f;

	public Vector3 maxSpawnValues;
	public Vector3 minSpawnValues;

	public Vector3 maxTargetPositionValues;
	public Vector3 minTargetPositionValues;

    void Start ()
    {
        foreground = GameObject.Find("Foreground");
        InvokeRepeating ("SpawnRalph", spawnTime, spawnTime);
        foreground = GameObject.Find("Foreground");
		//Invoke ("Spawn", spawnTime * 5f);
		InvokeRepeating ("TryToSpawn", spawnTime * 15f	, spawnTime);
    }

	void Update() {
		if (!Pause.paused) {
			timeSinceLastSpawn += Time.deltaTime;
		}
	}


    void TryToSpawn () {
		if (shouldSpawn ()) {
			SpawnRalph ();
		}
    }

    void SpawnRalph() {
        GameObject toSpawn = shouldBeNether () ? netherEnemy : enemy;
        GameObject spawnedEnemy = Instantiate (toSpawn, new Vector3 (0f, 0f, 0f), foreground.transform.rotation, foreground.transform);
        Ralph spawnedRalph = spawnedEnemy.GetComponent<Ralph>();

        Vector3 initialPosition = generatePoint(maxSpawnValues, minSpawnValues);
        Vector3 targetPosition  = generatePoint(maxTargetPositionValues, minTargetPositionValues);

        int maximumNumberOfShots = 7; // Magic number

        spawnedRalph.InitializeRalph(initialPosition, targetPosition, maximumNumberOfShots);

        spawnedRalph.transform.Rotate (0, 180, 0);
        spawnedRalph.transform.Translate (new Vector3 (0f, 0f, -4f), Space.World);
        timeSinceLastSpawn = 0f;
    }
	private bool shouldSpawn() {
		float timeSinceLastSpawnSquared = timeSinceLastSpawn * timeSinceLastSpawn;
		int toBeat = Random.Range (0, 100);
		return timeSinceLastSpawnSquared > toBeat && !Pause.paused;
	}

	private bool shouldBeNether() {
		return Random.Range (0, 2) == 1;
	}
    
    private Vector3 generatePoint(Vector3 maxValues, Vector3 minValues){
        float x = (float) Random.Range (minValues.x, maxValues.x) ;
		float y = (float) Random.Range (minValues.y, maxValues.y) ;
		float z = (float) Random.Range (minValues.z, maxValues.z) ;
		return new Vector3 (x, y, z); 	
    }
}
