using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    private float m_minSpeedToBreak;

	void Awake ()
    {
        m_minSpeedToBreak = 32;
    }
	
	void Update () {
		
	}

    private void Break()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public bool PlayerCollision(float verticalSpeed, Player player)
    {
        if (Mathf.Abs(verticalSpeed) >= m_minSpeedToBreak)
        {
            Break();
            return true;
        }
        else
        {
            return false;
        }
    }
}
