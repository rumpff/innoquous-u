﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int m_levelIndex;
    [SerializeField]
    private string m_levelName;

    public enum GameStates
    {
        PlayerInActive,
        PlayerAlive,
        PlayerDead,
        PlayerFinished
    };

    private GameStates m_currentGameState;

    private UIManager m_uiManager;
    private SFXManager m_sfxManager;

    private MainCamera m_mainCamera;

    private Vector2[] m_dimensions = new Vector2[]
    {
        Vector2.down,
        Vector2.right,
        Vector2.up,
        Vector2.left
    };

    private float m_gravity = 64;
    private int m_currentDimension;

    private int m_blueSwitch = 0;

    private float m_gameTime;

    private static int m_deaths;


    void Start ()
    {
        gameObject.name = "GameManager";

        m_uiManager = GetComponent<UIManager>();
        m_sfxManager = GameObject.Find("AudioManager").GetComponent<SFXManager>();
        

        m_currentDimension = 0;
        m_currentGameState = GameStates.PlayerInActive;
        m_gameTime = 0;

        m_mainCamera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
    }
    private void Update()
    {
        Physics.gravity = m_dimensions[m_currentDimension] * m_gravity;

        switch(GameState)
        {
            case GameStates.PlayerAlive:
                m_gameTime += Time.deltaTime;

                break;

            case GameStates.PlayerFinished:

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                { m_deaths = 0; } // Goto next level

                if (Input.GetKeyDown(KeyCode.R))
                { StartCoroutine(ReloadSceneAsync()); m_deaths = 0; }

                break;

            case GameStates.PlayerDead:

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                { StartCoroutine(ReloadSceneAsync()); m_deaths++; }

                break;
        }
    }

    public int LevelIndex
    {
        get { return m_levelIndex; }
    }
    public string LevelName
    {
        get { return m_levelName; }
    }

    public float GameTime
    {
        get { return m_gameTime; }
    }

    public int Deaths
    {
        get { return m_deaths; }
    }

    public UIManager UIManager
    {
        get { return m_uiManager; }
    }
    public SFXManager SFXManager
    {
        get { return m_sfxManager; }
    }

    public GameStates GameState
    {
        get { return m_currentGameState; }
        set { m_currentGameState = value; }
    }
    public MainCamera MainCamera
    {
        get { return m_mainCamera; }
    }

    public Vector2 GetDimension(bool plusOne)
    {
        if (!plusOne) { return m_dimensions[m_currentDimension]; }
        else
        {
            int pos = m_currentDimension + 1;
            if (pos >= m_dimensions.Length) { pos = 0; }
            return m_dimensions[pos];
        }
    }
    public int GetDimension()
    {
        return m_currentDimension;
    }
    public void AddDimension(int amount)
    {
        if (amount > 0)
        {
            if (m_currentDimension + 1 >= m_dimensions.Length)
            {
                m_currentDimension = 0;
            }
            else
            {
                m_currentDimension++;
            }
        }
        else
        {
            if (m_currentDimension - 1 < 0)
            {
                m_currentDimension = m_dimensions.Length - 1;
            }
            else
            {
                m_currentDimension--;
            }
        }

        SFXManager.PlaySound(SFXManager.Sounds.GravityFlip);
    }
    public int Dimension
    {
        get { return m_currentDimension; }
        set { m_currentDimension = value; }
    }

    public void ActivateBlueSwitch()
    {
        if (m_blueSwitch == 0)
        { m_blueSwitch = 1; }
        else { m_blueSwitch = 0; }
    }
    public int BlueSwitch
    {
        get { return m_blueSwitch; }
    }

    IEnumerator ReloadSceneAsync()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}