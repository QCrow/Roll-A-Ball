using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    //Player Stats
    public float speed = 10;
    public float maxHP = 100f;
    public float currentHP;

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