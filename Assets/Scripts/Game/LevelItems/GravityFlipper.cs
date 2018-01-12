using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipper : MonoBehaviour
{
    private GameObject[] m_flipperParts = new GameObject[4];
    private float m_partAngle;
    [SerializeField]
    private float m_partDistance;

    private bool m_active;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            m_flipperParts[i] = transform.GetChild(i).gameObject;
            m_flipperParts[i].transform.localPosition = new Vector3(0, 1, -0.5f);
        }

        m_partAngle = 0;
        m_active = false;

    }

    private void Update()
    {
        m_partAngle -= 8 * Time.deltaTime;

        for (int i = 0; i < 4; i++)
        {
            float x = Mathf.Cos(i * (Mathf.PI * 2) / 4 + m_partAngle) * m_partDistance;
            float y = Mathf.Sin(i * (Mathf.PI * 2) / 4 + m_partAngle) * m_partDistance;

            m_flipperParts[i].transform.localPosition = new Vector3(x, y);
            m_flipperParts[i].transform.eulerAngles = new Vector3(0, 0, m_partAngle * Mathf.Rad2Deg + (i * 90 + 45 - 180));
        }

        if (m_active) { m_partDistance = Mathf.Lerp(m_partDistance, 0.2f, 16 * Time.deltaTime); }
        else { m_partDistance = Mathf.Lerp(m_partDistance, 0, 12 * Time.deltaTime); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.PlayerState = Player.PlayerStates.inFlipper;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;

            m_active = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Player>().PlayerState == Player.PlayerStates.inFlipper)
            {
                other.transform.position = Vector3.Lerp(other.transform.position, transform.position, 9 * Time.deltaTime);        
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_active = false;
            other.GetComponent<Player>().SetStateToNormal();
        }
    }
}
