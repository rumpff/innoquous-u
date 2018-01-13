using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    private float m_minSpeedToBreak;
    private ParticleSystem m_particleSystem;

	void Awake ()
    {
        m_minSpeedToBreak = 32;
    }
	
	void Start ()
    {
        m_particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Break()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        m_particleSystem.Play();
        AudioManager.SFX.PlaySound(SFXManager.Sounds.BlockBreak);
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
