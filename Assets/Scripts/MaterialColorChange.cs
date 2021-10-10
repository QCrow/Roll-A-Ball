using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChange : MonoBehaviour
{
    [SerializeField] private Material myMaterial;

    private void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthUp"))
        {
            myMaterial.color = Color.red;
        }
    }
    private void OntriggerExit(Collider other)
    {
        if (other.CompareTag("HealthUp"))
        {
            myMaterial.color = Color.white;
        }
    }
}

