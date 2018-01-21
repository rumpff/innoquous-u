using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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
    private GameObject m_bulletPrefab;

    private bool m_gameStarted = false;
    private float m_shootTimer = 1;
    [SerializeField]
    private float m_delay;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_gameStarted = true;
        m_bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet");
        m_shootTimer += m_delay;
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, (int)m_direction * 90 + 180);
        if(m_shootTimer < 0)
        { Shoot(); }
        else
        { m_shootTimer -= Time.deltaTime; }
    }

    private void Shoot()
    {
        var b = Instantiate(m_bulletPrefab);
        b.GetComponent<Bullet>().CreateEvent(transform.position, (int)m_direction, transform.eulerAngles);
        m_shootTimer = 1;
    }
}
