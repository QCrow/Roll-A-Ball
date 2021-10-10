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

    float turretTimer;
    public float turretRate = 4f;
    public static int turretCount = 0;
    float blueExpTimer;
    float purpleExpTimer;
    float yellowExpTimer;
    float powerUpTimer;
    public static int pickUpCount;
    public float blueExpRate = 2f;
    public float purpleExpRate = 4f;
    public float yellowExpRate = 10f;
    public float powerUpRate = 8f;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = new GameObject[] { speedBoost, shield, lifePotion };
        player = GameObject.Find("Player");
        pickUpCount = 0;
        turretTimer = turretRate;
        blueExpTimer = blueExpRate;
        purpleExpTimer = purpleExpRate;
        yellowExpTimer = yellowExpRate;
        powerUpTimer = powerUpRate;

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
                pickUpCount++;
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
            turretTimer = turretRate * (Mathf.Pow(2f, turretCount/4));
        }
        if (blueExpTimer <= 0)
        {
            RandomSpawn(blueExp);
            blueExpTimer = blueExpRate * (Mathf.Pow(1.5f, pickUpCount / 20));
        }
        if (purpleExpTimer <= 0)
        {
            RandomSpawn(purpleExp);
            purpleExpTimer = purpleExpRate * (Mathf.Pow(1.5f, pickUpCount / 20));
        }
        if (yellowExpTimer <= 0)
        {
            RandomSpawn(yellowExp);
            yellowExpTimer = yellowExpRate * (Mathf.Pow(1.5f, pickUpCount / 20));
        }
        if (powerUpTimer <= 0)
        {
            SpawnPowerUp();
            powerUpTimer = powerUpRate;
        }
        turretTimer -= Time.deltaTime;
        blueExpTimer -= Time.deltaTime;
        purpleExpTimer -= Time.deltaTime;
        yellowExpTimer -= Time.deltaTime;
        powerUpTimer -= Time.deltaTime;

    }
}
