using System.Collections;
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

    private Player m_player;

    private UIManager m_uiManager;

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

        m_currentDimension = 0;
        m_currentGameState = GameStates.PlayerInActive;
        m_gameTime = 0;

        m_mainCamera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        m_player = GameObject.Find("Player").GetComponent<Player>();
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

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) // Goto next level
                {
                    m_deaths = 0;

                    var sceneCount = SceneManager.sceneCountInBuildSettings;
                    var currentScene = SceneManager.GetActiveScene().buildIndex;

                    if ((SceneManager.GetActiveScene().buildIndex + 1) < sceneCount) // Next scene exists
                    {
                        SceneManager.LoadScene(currentScene + 1);
                        
                    }
                    else // No more scenes to go to
                    {
                        SceneManager.LoadScene(0); // Go to the main menu instead
                    }
                } 

                if (Input.GetKeyDown(KeyCode.Escape)) // Goto main menu
                { m_deaths = 0; SceneManager.LoadScene(0); }

                if (Input.GetKeyDown(KeyCode.R)) // Restart level
                { StartCoroutine(ReloadSceneAsync()); m_deaths = 0; }

                break;

            case GameStates.PlayerDead:

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                { StartCoroutine(ReloadSceneAsync()); m_deaths++; }

                if (Input.GetKeyDown(KeyCode.Escape)) // Goto main menu
                { m_deaths = 0; SceneManager.LoadScene(0); }

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

    public Player Player
    {
        get { return m_player; }
    }

    public UIManager UIManager
    {
        get { return m_uiManager; }
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

    public Vector2 GetDimension()
    {
        return m_dimensions[m_currentDimension];
    }

    public int GetDimensionIndex()
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

        AudioManager.SFX.PlaySound(SFXManager.Sounds.GravityFlip);
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