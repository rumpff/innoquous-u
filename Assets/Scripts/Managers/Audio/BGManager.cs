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
        // Get the component
        m_audioSource = gameObject.AddComponent<AudioSource>();
        m_audioSource.playOnAwake = false;

        // Load all the content of the bgm folder in the array
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
