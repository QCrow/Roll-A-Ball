using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackingsystem : MonoBehaviour
{

    public float speed = 8.0f;
    GameObject m_target = null;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        m_target = GameObject.Find("Player");
    }
    void Update()
    {
        if (m_target)
        {
            Vector3 m_target2D = new Vector3(m_target.transform.position.x, 0.5f, m_target.transform.position.z);
            if (m_lastKnownPosition != m_target2D)
            {

                m_lastKnownPosition = m_target2D;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);

            }
            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
            }
        }



    }
    bool SetTarget(GameObject target)
    {
        if (target)
        {
            return false;
        }
        m_target = target;

        return true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            Destroy(gameObject);
            RandomSpawner.turretCount--;
        }

    }
}
