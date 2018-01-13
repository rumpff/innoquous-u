using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public enum Sounds
    {
        PlayerJump,
        PlayerDeath,
        GravityFlip,
        Switch,
        BlockBreak,
        SecretObtained,
        LevelEnter,
        LevelComplete,
        MenuEnter,
        MenuSelect,
        MenuClick,
        IntroPause
    }

    private AudioSource[] m_audioSources;

    private void Awake()
    {
        int enumLength = Enum.GetNames(typeof(Sounds)).Length;
        m_audioSources = new AudioSource[enumLength];

        for (int i = 0; i < enumLength; i++)
        {
            Sounds sounds = (Sounds)i;
            string stringValue = sounds.ToString();

            m_audioSources[i] = gameObject.AddComponent<AudioSource>() as AudioSource;

            m_audioSources[i].clip = (AudioClip)Resources.Load("Audio/SFX/" + stringValue);
            m_audioSources[i].playOnAwake = false;
        }
    }

    public void PlaySound(Sounds sound)
    {
        int index = (int)sound;
        m_audioSources[index].Stop();
        m_audioSources[index].Play();
    }
}
