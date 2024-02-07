using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource source;
    private AudioSource music;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        music = transform.GetChild(0).GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void ChangedSound(float _changed)
    {
        ChangedVolume(1, "soundVolume", _changed, source);
    }

    public void ChangedMusic(float _changed)
    {
        ChangedVolume(0.5f, "musicVolume", _changed, music);
    }

    private void ChangedVolume(float baseVolume, string volumeName, float changed, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName,1);
        currentVolume += changed;

        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        source.volume = currentVolume * baseVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
