using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        PlayerController playercontroller = player.GetComponent<PlayerController>();

        if (playercontroller.currentHP <= 0)
        {
            playercontroller.currentHP = playercontroller.maxHP;
            player.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
            Physics.SyncTransforms();
            rb.velocity = new Vector3(0f, 0f, 0f);
            playercontroller.speedBoosted = false;
            playercontroller.shieldUpDuration = 2.0f;
            playercontroller.shieldUp = true;
        }
    }
}
