using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    private AudioSource m_audioSource;
    private Object[] m_bgmArray;

    private int m_currentSongIndex;

    private void Awake()
    {
        // Check if there'sn't already an instance of the audiomanager
        if (FindObjectsOfType(typeof(BGManager)).Length > 1)
        { Destroy(gameObject); }

        // Make sure that the object stays when loading a new scene
        DontDestroyOnLoad(transform.gameObject);

        // Get the component
        m_audioSource = gameObject.AddComponent<AudioSource>();
        m_audioSource.playOnAwake = false;

        m_bgmArray = Resources.LoadAll("Audio/BGM");
    }

    public void Update()
    {
        if (!m_audioSource.isPlaying)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (m_audioSource.isPlaying) return;

        int newSongIndex = m_currentSongIndex;

        while(newSongIndex == m_currentSongIndex)
        { newSongIndex = Random.Range(0, m_bgmArray.Length - 1); }

        m_audioSource.clip = (AudioClip)m_bgmArray[newSongIndex];
        m_audioSource.Play();

        m_currentSongIndex = newSongIndex;
    }

    public void StopMusic()
    {
        m_audioSource.Stop();
    }

    public void PauseMusic()
    {
        m_audioSource.Pause();
    }

    public void ResumeMusic()
    {
        m_audioSource.UnPause();
    }
}
