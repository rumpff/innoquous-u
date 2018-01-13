using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static BGManager m_bgmManager;
    private static SFXManager m_sfxManager;

    private void Awake()
    {
        // Check if there'sn't already an instance of the audiomanager
        if (FindObjectsOfType(typeof(BGManager)).Length > 1)
        { Destroy(gameObject); }

        // Make sure that the object stays when loading a new scene
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        // Assign the components to the variables
        m_bgmManager = GetComponent<BGManager>();
        m_sfxManager = GetComponent<SFXManager>();
    }

    public static BGManager BGM
    {
        get { return m_bgmManager; }
    }
    public static SFXManager SFX
    {
        get { return m_sfxManager; }
    }
}
