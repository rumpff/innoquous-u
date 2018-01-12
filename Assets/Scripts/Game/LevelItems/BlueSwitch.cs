using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSwitch : MonoBehaviour
{
    private GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_gameManager.ActivateBlueSwitch();  
        }
    }
}
