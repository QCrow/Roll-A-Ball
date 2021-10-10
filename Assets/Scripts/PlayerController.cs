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
    public float jumpForce = 1.5f;
    public float speedBoostEff = 2f;
    public float speedBoostDuration = 2f;
    public float shieldUpDuration = 10f;
    public float healingEff = 0.3f;

    //Kinematics
    Rigidbody rb;
    float movementX;
    float movementY;

    //Jump
    Vector3 jump;
    bool jumping;

    private float speedBoostPercent;
    [HideInInspector] public bool speedBoosted = false;
    [HideInInspector] public bool shieldUp = false;
    float speedBoostTime;
    float shieldUpTime;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;
    }


    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    

    //Jumping Related Methods
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

        if (shieldUpTime <= 0)
        {
            shieldUp = false;
            shieldUpTime = shieldUpDuration;
        }

        if (shieldUp)
        {
            shieldUpTime = shieldUpTime - Time.deltaTime;
        }
    }

    //Kinematics
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY) * speedBoostPercent;
        rb.AddForce(movement * speed * speedBoostPercent);
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        if (!speedBoosted)
        {
            speedBoostPercent = 1f;
        }
    }

    


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            Destroy(other.gameObject);
            if (!speedBoosted)
            {
                speedBoosted = true;
                speedBoostTime = speedBoostDuration;
                speedBoostPercent = speedBoostEff;
            }

            if (speedBoosted)
            {
                speedBoostTime = speedBoostDuration;
            }
        }

        if (other.gameObject.CompareTag("ShieldUp"))
        {
            Destroy(other.gameObject);
            if (!shieldUp)
            {
                shieldUp = true;
                shieldUpTime = shieldUpDuration;
            }

            if (shieldUp)
            {
                if (other.gameObject.CompareTag("Projectile"))
                {
                    Destroy(other.gameObject);
                }
            }
        }

        if (other.gameObject.CompareTag("HealthUp"))
        {
            Destroy(other.gameObject);
            if (currentHP + healingEff * maxHP <= maxHP)
            {
                currentHP = currentHP + healingEff * maxHP;
            }
            else
            {
                currentHP = maxHP;
            }
        }

        if (!shieldUp)
        {
            if (other.gameObject.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
                currentHP -= ShootingSystem.projectileDamage;
            }
        }
       
    }
}