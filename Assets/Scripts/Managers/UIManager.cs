using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    private GameManager m_gameManager;

    private RectTransform m_gameUIGroup;
    private float m_gameUILeftOffset, m_gameUIRightOffset;

    private GameObject m_deathUIGroup, m_finishedUIGroup, m_tip;
    private TextMeshProUGUI m_deathCouseText;
    private string m_deathCause;

    private Image m_blackBG;
    private Image[] m_healthBlocks;

    private bool m_statsUpdated = false;
    void Start()
    {
        m_gameManager = GetComponent<GameManager>();

        m_gameUIGroup = GameObject.Find("GameUIGroup").GetComponent<RectTransform>();
        m_gameUILeftOffset = -640;
        m_gameUIRightOffset = 0;

        m_blackBG = GameObject.Find("BlackBG").GetComponent<Image>();
        m_deathUIGroup = GameObject.Find("DeathUIGroup");
        m_deathCouseText = GameObject.Find("DeathCauseText").GetComponent<TextMeshProUGUI>();

        var healthGroup = GameObject.Find("HealthGroup");
        var healthCount = healthGroup.transform.childCount;

        m_healthBlocks = new Image[healthCount];

        for (int i = 0; i < healthCount; i++)
        {
            m_healthBlocks[i] = healthGroup.transform.GetChild(i).GetComponent<Image>();
        }

        m_tip = GameObject.Find("InfoTip");

        m_finishedUIGroup = GameObject.Find("LevelEndUIGroup");

        GameObject.Find("LevelNameText").GetComponent<TextMeshProUGUI>().text = m_gameManager.LevelName;
        GameObject.Find("LevelNumbText").GetComponent<TextMeshProUGUI>().text = m_gameManager.LevelIndex.ToString("000");

        m_blackBG.color = new Color(0, 0, 0, 0);

        DisableTip();
    }

    void Update()
    {
        // Basic switch for normal states
        switch (m_gameManager.GameState)
        {
            case GameManager.GameStates.PlayerInActive:
                GameUIVisible(true);
                m_deathUIGroup.SetActive(false);
                m_finishedUIGroup.SetActive(false);
                break;

            case GameManager.GameStates.PlayerAlive:
                GameUIVisible(true);
                m_deathUIGroup.SetActive(false);
                m_finishedUIGroup.SetActive(false);
                UpdateHealthbar();
                break;

            case GameManager.GameStates.PlayerDead:
                GameUIVisible(false);
                m_deathUIGroup.SetActive(true);
                m_deathCouseText.text = "by " + m_deathCause;
                break;

            case GameManager.GameStates.PlayerFinished:
                GameUIVisible(false);
                m_finishedUIGroup.SetActive(true);
                SetStats();

                if (m_gameManager.MainCamera.FinishTimer >= 1)
                {
                    m_blackBG.color = new Color(0, 0, 0, 255);
                }
                break;
        }
    }

    public void SetDeathCouse(string cause)
    {
        m_deathCause = cause;
    }

    private void SetStats()
    {
        if (!m_statsUpdated)
        {
            GameObject.Find("LevelNameStat").GetComponent<TextMeshProUGUI>().text = m_gameManager.LevelName;
            GameObject.Find("LevelNumbTextEnd").GetComponent<TextMeshProUGUI>().text = m_gameManager.LevelIndex.ToString("000");
            GameObject.Find("LevelNumbStat").GetComponent<TextMeshProUGUI>().text = m_gameManager.LevelIndex.ToString("000");

            string time = m_gameManager.GameTime.ToString("00.00");
            time = time.Replace(".", ":");
            GameObject.Find("TimeStat").GetComponent<TextMeshProUGUI>().text = time;
            GameObject.Find("DeathStat").GetComponent<TextMeshProUGUI>().text = m_gameManager.Deaths.ToString();

            m_statsUpdated = true;
        }
    }

    private void GameUIVisible(bool visible)
    {
        if (visible)
        {
            m_gameUILeftOffset = Mathf.Lerp(m_gameUILeftOffset, 0, 6 * Time.deltaTime);
        }
        else
        {
            m_gameUILeftOffset = Mathf.Lerp(m_gameUILeftOffset, -640, 6 * Time.deltaTime);
        }

        m_gameUIGroup.offsetMin = new Vector2(m_gameUILeftOffset, 0);
        m_gameUIGroup.offsetMax = new Vector2(0, 0);
    }

    private void UpdateHealthbar()
    {
        var health = m_gameManager.Player.Health;

        for (int i = 0; i < m_healthBlocks.Length; i++)
        {
            var alpha = (health * 10 - i);
            m_healthBlocks[i].color = new Color(1, 1, 1, alpha);
        }
    }


    public void EnableTip(string text)
    {
        m_tip.SetActive(true);
        m_tip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    public void DisableTip()
    {
        m_tip.SetActive(false);
    }
}
