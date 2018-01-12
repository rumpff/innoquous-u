using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{

    private GameManager m_gameManager;
    private ParticleSystem m_explosionParticleSytem;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.name = "FinishBlock";

        m_explosionParticleSytem = GetComponent<ParticleSystem>();
        m_explosionParticleSytem.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SetActive(false);
            m_gameManager.GameState = GameManager.GameStates.PlayerFinished;
            m_gameManager.SFXManager.PlaySound(SFXManager.Sounds.LevelComplete);

            m_explosionParticleSytem.Play();
        }
    }
}
