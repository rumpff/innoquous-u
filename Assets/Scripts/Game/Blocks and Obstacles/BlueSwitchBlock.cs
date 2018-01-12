using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSwitchBlock : MonoBehaviour
{
    private GameManager m_gameManager;

    [SerializeField]
    private bool m_startingPosition;

    private Vector3 m_activePos;
    private Vector3 m_InactivePos;

    void Start ()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        m_activePos = transform.position;
        m_InactivePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
    }
	
	void Update ()
    {
        Vector3 Dest;
        if (m_startingPosition)
        {
            if (m_gameManager.BlueSwitch == 0)
            { Dest = m_activePos; }
            else
            { Dest = m_InactivePos; }
        }
        else
        {
            if (m_gameManager.BlueSwitch == 1)
            { Dest = m_activePos; }
            else
            { Dest = m_InactivePos; }
        }

        transform.position = Vector3.Lerp(transform.position, Dest, 10 * Time.deltaTime);
	}
}
