﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string m_helpText;
    private GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_gameManager.UIManager.EnableTip(m_helpText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_gameManager.UIManager.DisableTip();
        }
    }
}
