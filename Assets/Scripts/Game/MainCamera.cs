using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve m_finnishZoomCurve;

    private GameManager m_gameManager;
    private GameObject m_player;

    private float m_rotateCurrent, m_rotateDest,
        m_zDistance;

    private float m_finishTimer;

    private void Awake()
    {
        m_rotateDest = 0;
        m_rotateCurrent = 0;
        m_zDistance = -40;

        m_finishTimer = 0;
    }

    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_player = GameObject.Find("Player");

        transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, m_zDistance);
    }

    private void Update()
    {
        if (m_gameManager.GameState == GameManager.GameStates.PlayerFinished)
        {
            if (m_player.name == "Player")
            { m_player = GameObject.Find("FinishBlock"); }

            m_zDistance = m_finnishZoomCurve.Evaluate(m_finishTimer) * -15;
            if (m_finishTimer < 1) { m_finishTimer += Time.deltaTime; }
        }
        else
        {
            m_zDistance = Mathf.Lerp(m_zDistance, -15, 8 * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        m_rotateDest = m_gameManager.GetDimension() * 90;
        m_rotateCurrent = Mathf.LerpAngle(m_rotateCurrent, m_rotateDest, 10 * Time.deltaTime);

        Vector3 pos = Vector3.Lerp(transform.position, m_player.transform.position, 7 * Time.deltaTime);
        transform.position = new Vector3(pos.x, pos.y, m_zDistance);
        transform.eulerAngles = new Vector3(0, 0, m_rotateCurrent + m_finnishZoomCurve.Evaluate(m_finishTimer) * 10 - 10);
    }

    public float FinishTimer
    {
        get { return m_finishTimer; }
    }
}
