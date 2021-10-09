using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeRotator : MonoBehaviour
{
    Transform Target;



    private void Start()
    {
        Target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        Vector3 direction = Target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction + new Vector3(0f, 90f, 0f));
        transform.rotation = rotation;
    }
}
