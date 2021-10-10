using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Player Stats
    public float speed = 10;
    public float maxHP = 100f;
    public float currentHP;



    //Game Objects
    int pickUpCount;
    public GameObject blueExp;
    public GameObject purpleExp;
    public GameObject yellowExp;
    public GameObject speedBoost;
    public GameObject shield;
    public GameObject lifePotion;
    GameObject[] powerUps;
    

    //Kinematics
    Rigidbody rb;
    float movementX;
    float movementY;

    //Jump
    Vector3 jump;
    public float jumpForce = 1.5f;
    bool jumping;

    //Speed boost parameters
    public float speedBoostDuration = 2f;
    public float speedBoostPercent = 2f;
    public bool speedBoosted = false;
    float speedBoostTime;


    //Spawns parameters
    float blueExpTimer = 1f;
    float purpleExpTimer = 3f;
    float yellowExpTimer = 10f;
    float powerUpTimer = 8f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickUpCount = 0;

        powerUps = new GameObject[] {speedBoost, shield, lifePotion};

        currentHP = maxHP;
    }

    void RandomSpawn(GameObject spawn)
    {
        if (pickUpCount < 100)
        {
            bool spawned = false;
            while (!spawned)
            {
                Vector3 spawnLocation = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));

                if ((spawnLocation - this.transform.position).magnitude < 3)
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
    }

    void SpawnPowerUp()
    {
        bool powerUpSpawned = false;
        while (!powerUpSpawned)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));

            if ((spawnLocation - this.transform.position).magnitude < 3)
            {
                continue;
            }
            else
            {
                Instantiate(powerUps[Random.Range(0,powerUps.Length)], spawnLocation, Quaternion.identity);
                powerUpSpawned = true;
            }
        }
    }

    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if (!jumping)
        {
            jump = new Vector3(0.0f, 2.0f, 0.0f);
        }
    }

    private void OnCollisionStay()
    {
        jumping = false;
    }

    private void OnCollisionExit()
    {
        jumping = true;
        jump = new Vector3(0f,0f,0f);
    }

    void Update()
    {
        //HP if out of map
        if (transform.position.y <= -1)
        {
            currentHP -= 100 * Time.deltaTime;
        }
        //Random Spawning
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

        blueExpTimer -= Time.deltaTime;
        purpleExpTimer -= Time.deltaTime;
        yellowExpTimer -= Time.deltaTime;
        powerUpTimer -= Time.deltaTime;

        //powerUps time track
        if (speedBoostTime <= 0)
        {
            speedBoosted = false;
            speedBoostTime = speedBoostDuration;
        }

        if (speedBoosted)
        {
            speedBoostTime = speedBoostTime - Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (!speedBoosted)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
        if (speedBoosted)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY) * speedBoostPercent;
            rb.AddForce(movement * speed * speedBoostPercent);
        }
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    }

    


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            pickUpCount--;
        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            Destroy(other.gameObject);
            if (!speedBoosted)
            {
                speedBoosted = true;
                speedBoostTime = speedBoostDuration;
            }

            if (speedBoosted)
            {
                speedBoostTime = speedBoostDuration;
            }
        }

        if (other.gameObject.CompareTag("ShieldUp"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("HealthUp"))
        {
            Destroy(other.gameObject);
        }
    }
}