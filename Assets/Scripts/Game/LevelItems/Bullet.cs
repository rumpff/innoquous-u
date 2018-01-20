using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3[] m_bulletDirections = new Vector3[]
    { Vector3.down, Vector3.right, Vector3.up, Vector3.left };

    private int m_direction;
    private float m_moveSpeed = 0.3f;

    public void CreateEvent(Vector3 pos, int direction, Vector3 eulerRotation)
    {
        transform.position = pos;
        m_direction = direction;
        transform.eulerAngles = eulerRotation;
    }

    private void FixedUpdate()
    {
        transform.position += m_bulletDirections[m_direction] * m_moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (m_direction - 1 < 0)
            {
                m_direction = m_bulletDirections.Length - 1;
            }
            else
            {
                m_direction--;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
