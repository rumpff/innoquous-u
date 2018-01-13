using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GravityArrow : MonoBehaviour
{
    public enum Directions
    {
        down,
        right,
        up,
        left
    }

    [SerializeField]
    private Directions m_direction;
    private GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, (int)m_direction * 90);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_gameManager.Dimension != (int)m_direction)
            {
                m_gameManager.Dimension = (int)m_direction;
                AudioManager.SFX.PlaySound(SFXManager.Sounds.GravityFlip);
            }
        }
    }
}
