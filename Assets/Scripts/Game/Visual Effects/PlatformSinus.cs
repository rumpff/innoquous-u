using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSinus : MonoBehaviour
{
    private float m_zOriginal;
    private float m_zOffset;
    private float m_sinNumb;

	void Start ()
    {
        m_zOriginal = transform.position.z;
        m_zOffset = 0;
        m_sinNumb = (transform.position.x + transform.position.y) * 0.5f;
    }


    void Update ()
    {
        m_sinNumb += 3.33f * Time.deltaTime; //2
        m_zOffset = Mathf.Sin(m_sinNumb) * 0.2f;

        transform.position = new Vector3(transform.position.x, transform.position.y, m_zOriginal + m_zOffset);
    }
}
