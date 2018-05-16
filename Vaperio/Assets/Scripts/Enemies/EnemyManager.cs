using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public GameObject netherEnemy;                // The enemy prefab to be spawned.
    public float spawnTime = 1f;            // How long between each spawn.
    public int ralphsKilled = 0;
	private GameObject foreground;
	public Vector3 minSpawnValues;
	public Vector3 maxSpawnValues;
	private float timeSinceLastSpawn = 0f;
	private float sessionStart = 0f;

    void Start ()
    {        
        foreground = GameObject.Find("Foreground");
		Invoke ("SpawnNormal", spawnTime * 4.8f);
		sessionStart = Time.time;
    }

	void Update() {
		if (!Pause.paused) {
			timeSinceLastSpawn += Time.deltaTime;
		}
	}


    void TryToSpawn () {
		if (shouldSpawn ()) {
			Spawn (shouldBeNether());
		}
    }

    void SpawnNether()
    {
        Spawn(true);
    }

    void SpawnNormal()
    {
        Spawn(false);
    }

	void Spawn(bool nether) {
		GameObject toSpawn = nether ? netherEnemy : enemy;
		GameObject spawnedEnemy = Instantiate (toSpawn, new Vector3 (0f, 0f, 0f), foreground.transform.rotation, foreground.transform);
		spawnedEnemy.transform.Rotate (0, 180, 0);
		spawnedEnemy.transform.Translate (generatePoint ());
		spawnedEnemy.transform.Translate (new Vector3 (0f, 0f, -4f), Space.World);
		spawnedEnemy.layer = LayerMask.NameToLayer ("Enemies");
		timeSinceLastSpawn = 0f;
	}

    public void EnemyKilled()
    {
        ralphsKilled++;
        if(ralphsKilled == 1)
        {
            Invoke("SpawnNether", 4f);
        } else if (ralphsKilled == 2)
        {
            InvokeRepeating("TryToSpawn", spawnTime * 4f, spawnTime);
        }
    }

	private bool shouldSpawn() {
		float timeSinceLastSpawnSquared = timeSinceLastSpawn * timeSinceLastSpawn;
		int toBeat = Random.Range (0, 100);
		timeSinceLastSpawnSquared += (Time.time - sessionStart) * 0.1f;
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