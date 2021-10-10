using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //Game Objects
    public GameObject player;
    public GameObject turret;
    public GameObject blueExp;
    public GameObject purpleExp;
    public GameObject yellowExp;
    public GameObject speedBoost;
    public GameObject shield;
    public GameObject lifePotion;
    GameObject[] powerUps;

    //Spawns parameters
    float turretTimer = 5f;
    public static int turretCount = 0;
    float blueExpTimer = 1f;
    float purpleExpTimer = 3f;
    float yellowExpTimer = 10f;
    float powerUpTimer = 8f;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = new GameObject[] { speedBoost, shield, lifePotion };
        player = GameObject.Find("Player");
    }

    void RandomSpawn(GameObject spawn)
    {
        bool spawned = false;
        while (!spawned)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));

            if ((spawnLocation - player.transform.position).magnitude < 3)
            {
                continue;
            }
            else
            {
                Instantiate(spawn, spawnLocation, Quaternion.identity);
                spawned = true;
            }
        }
    }

    void SpawnPowerUp()
    {
        bool powerUpSpawned = false;
        while (!powerUpSpawned)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));

            if ((spawnLocation - player.transform.position).magnitude < 3)
            {
                continue;
            }
            else
            {
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnLocation, Quaternion.identity);
                powerUpSpawned = true;
            }
        }
    }

    void TurretSpawn()
    {
        if (turretCount <= 2)
        {
            bool spawned = false;
            while (!spawned)
            {
                Vector3 spawnLocation = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));

                if ((spawnLocation - player.transform.position).magnitude < 3)
                {
                    continue;
                }
                else
                {
                    Instantiate(turret, spawnLocation, Quaternion.identity);
                    spawned = true;
                }
            }
            turretCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Random Spawning
        if (turretTimer <= 0)
        {
            TurretSpawn();
            turretTimer = 5f;
        }
        if (blueExpTimer <= 0)
        {
            RandomSpawn(blueExp);
            blueExpTimer = 1f;
        }
        if (purpleExpTimer <= 0)
        {
            RandomSpawn(purpleExp);
            purpleExpTimer = 3f;
        }
        if (yellowExpTimer <= 0)
        {
            RandomSpawn(yellowExp);
            yellowExpTimer = 10f;
        }
        if (powerUpTimer <= 0)
        {
            SpawnPowerUp();
            powerUpTimer = 8f;
        }
        turretTimer -= Time.deltaTime;
        blueExpTimer -= Time.deltaTime;
        purpleExpTimer -= Time.deltaTime;
        yellowExpTimer -= Time.deltaTime;
        powerUpTimer -= Time.deltaTime;

    }
}
