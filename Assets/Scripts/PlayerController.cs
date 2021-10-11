using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public HealthBar healthbar;

    //Player Stats
    public float speed = 10;
    public float maxHP = 100f;   
    public float currentHP;   
    public float jumpForce = 1.5f;
    public float speedBoostEff = 2f;
    public float speedBoostDuration = 2f;
    public float shieldUpDuration = 10f;
    public float healingEff = 0.10f;
    public static int score;
    public TextMeshProUGUI scoreText;

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
    [HideInInspector] public float shieldUpTime;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;
        healthbar.SetMaxHealth(maxHP);
        score = 0;
        Setscore();


    }


    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Setscore()
    {
        scoreText.text = "score: " + score.ToString();
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

        healthbar.SetHealth(currentHP);

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
        if (other.gameObject.CompareTag("PickUpBlue"))
        {
            score += 3;
            Destroy(other.gameObject);
<<<<<<< Updated upstream
            RandomSpawner.pickUpCount--;
=======
            Setscore();
>>>>>>> Stashed changes
        }

        if (other.gameObject.CompareTag("PickUpPurple"))
        {
            score += 10;
            Destroy(other.gameObject);
<<<<<<< Updated upstream
            RandomSpawner.pickUpCount--;
=======
            Setscore();
>>>>>>> Stashed changes
        }

        if (other.gameObject.CompareTag("PickUpYellow"))
        {
            score += 25;
            Destroy(other.gameObject);
<<<<<<< Updated upstream
            RandomSpawner.pickUpCount--;
=======
            Setscore();
>>>>>>> Stashed changes
        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            score += 15;
            Destroy(other.gameObject);
            Setscore();
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
            score += 20;
            Setscore();
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
            score += 10;
            Setscore();
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

        
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            if (!shieldUp)
            {
                currentHP -= ShootingSystem.projectileDamage;
            }
        }
       
    }
}