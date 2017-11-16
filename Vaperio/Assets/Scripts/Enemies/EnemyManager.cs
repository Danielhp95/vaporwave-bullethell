using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public GameObject netherEnemy;                // The enemy prefab to be spawned.
    private float netherSpawnProbability;
    private int maximumEnemiesToSpawn;
    private int enemiesSpawned;

    public float spawnTime;            // How long between each spawn.
	private GameObject foreground;
	private float timeSinceLastSpawn;

	private Vector3 maxSpawnValues;
	private Vector3 minSpawnValues;

	private Vector3 maxTargetPositionValues;
	private Vector3 minTargetPositionValues;

    void Start ()
    {
        this.foreground = GameObject.Find("Foreground");

        this.timeSinceLastSpawn = 0;
        this.enemiesSpawned = 0;

        // Enemy Spawner example
        Vector3 maxSpawn  = new Vector3(4f, 2.5f, 0f);
        Vector3 minSpawn  = new Vector3(4f, 2.5f, 0f);
        Vector3 maxTarget = new Vector3(3f, 2.5f, 0f);
        Vector3 minTarget = new Vector3(3f, 2.5f, 0f);
        InitializeEnemySpawner("Prefabs/NetherRalph", "Prefabs/NetherRalph", 10, 3f, 1.0f,
                                maxSpawn, minSpawn, maxTarget, minTarget);
    }

    void InitializeEnemySpawner(string enemy, string netherEnemy, int maximumEnemiesToSpawn, float spawnTime, float netherSpawnProbability,
                                Vector3 maxSpawnValues, Vector3 minSpawnValues,  Vector3 maxTargetPositionValues, Vector3 minTargetPositionValues)
    { 
        this.enemy = Resources.Load(enemy) as GameObject;
        this.netherEnemy = Resources.Load(netherEnemy) as GameObject;
        this.maxSpawnValues = maxSpawnValues;
        this.minSpawnValues = minSpawnValues;
        this.maxTargetPositionValues = maxTargetPositionValues;
        this.minTargetPositionValues = minTargetPositionValues;

        if (maximumEnemiesToSpawn <= 0)
        {
            print(string.Format("MaximumEnemiesToSpawn must be positive. Input {0}", maximumEnemiesToSpawn));
        }
        this.maximumEnemiesToSpawn = maximumEnemiesToSpawn;

        if (netherSpawnProbability > 1 || netherSpawnProbability < 0) {
            print(string.Format("Nether spawn probability should be in the range [0,1]. Input: {0}", netherSpawnProbability));
        }
        this.netherSpawnProbability = netherSpawnProbability;
        this.spawnTime = spawnTime;
    }

	void Update() {
		if (!Pause.paused) {
			timeSinceLastSpawn += Time.deltaTime;
		}
        if (this.enemiesSpawned < maximumEnemiesToSpawn) {
            if (shouldSpawn()) {
                Spawn();
                enemiesSpawned++;
            }
        }
	}

    void Spawn() {
        GameObject toSpawn = shouldBeNether () ? netherEnemy : enemy;
        GameObject spawnedEnemy = Instantiate (toSpawn, new Vector3 (0f, 0f, 0f), foreground.transform.rotation, foreground.transform);
        Ralph spawnedRalph = spawnedEnemy.GetComponent<Ralph>();

        Vector3 initialPosition = generatePoint(maxSpawnValues, minSpawnValues);
        Vector3 targetPosition  = generatePoint(maxTargetPositionValues, minTargetPositionValues);

        int maximumNumberOfShots = 7; // Magic number

        // THIS SHOULD CHANGE FOR EVERY TYPE OF ENEMY.
        // Consider overriding spawn function everytime.
        spawnedRalph.InitializeRalph(initialPosition, targetPosition, maximumNumberOfShots);

        spawnedRalph.transform.Rotate (0, 180, 0);
        spawnedRalph.transform.Translate (new Vector3 (0f, 0f, -4f), Space.World);
        timeSinceLastSpawn = 0f;
    }

	private bool shouldSpawn() {
		return this.timeSinceLastSpawn >= this.spawnTime && !Pause.paused;
	}

	private bool shouldBeNether() {
		return Random.Range (0, 1) < this.netherSpawnProbability;
	}
    
    private Vector3 generatePoint(Vector3 maxValues, Vector3 minValues){
        float x = (float) Random.Range (minValues.x, maxValues.x) ;
		float y = (float) Random.Range (minValues.y, maxValues.y) ;
		float z = (float) Random.Range (minValues.z, maxValues.z) ;
		return new Vector3 (x, y, z); 	
    }
}
