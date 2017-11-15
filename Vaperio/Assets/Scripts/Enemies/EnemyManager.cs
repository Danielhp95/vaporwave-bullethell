using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public GameObject netherEnemy;                // The enemy prefab to be spawned.
    public float spawnTime = 1f;            // How long between each spawn.
	private GameObject foreground;
	public Vector3 minSpawnValues;
	public Vector3 maxSpawnValues;
	private float timeSinceLastSpawn = 0f;

    void Start ()
    {        
        foreground = GameObject.Find("Foreground");
		Invoke ("Spawn", spawnTime * 5f);
		InvokeRepeating ("TryToSpawn", spawnTime * 15f	, spawnTime);
    }

	void Update() {
		if (!Pause.paused) {
			timeSinceLastSpawn += Time.deltaTime;
		}
	}


    void TryToSpawn () {
		if (shouldSpawn ()) {
			Spawn ();
		}
    }

	void Spawn() {
		GameObject toSpawn = shouldBeNether () ? netherEnemy : enemy;
		GameObject spawnedEnemy = Instantiate (toSpawn, new Vector3 (0f, 0f, 0f), foreground.transform.rotation, foreground.transform);
		spawnedEnemy.transform.Rotate (0, 180, 0);
		spawnedEnemy.transform.Translate (generatePoint ());
		spawnedEnemy.transform.Translate (new Vector3 (0f, 0f, -4f), Space.World);
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
    
    private Vector3 generatePoint(){
        float x = (float) Random.Range (minSpawnValues.x, maxSpawnValues.x) ;
		float y = (float) Random.Range (minSpawnValues.y, maxSpawnValues.y) ;
		float z = (float) Random.Range (minSpawnValues.z, maxSpawnValues.z) ;
		return new Vector3 (x, y, z); 	
    }
}