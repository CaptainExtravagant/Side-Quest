using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    float musicVolume;
    float sfxVolume;

    AudioMixer mixer;
    GameManager gameManager;

    public void Init(GameManager manager)
    {
        Debug.Log("Audio Manager Init");

        gameManager = manager;

        mixer = Resources.Load("Audio/MainSoundMixer") as AudioMixer;

        musicVolume = 0f;
        sfxVolume = 0f;

        mixer.SetFloat("MusicVolume", musicVolume);
        
        mixer.SetFloat("SFXVolume", sfxVolume);
    }

    public void MusicVolume(float newVolume)
    {
        if (newVolume == 0)
        {
            musicVolume = 0.01f;
        }
        else
        {
            musicVolume = newVolume;
        }
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void SFXVolume(float newVolume)
    {
        if (newVolume == 0)
        {
            sfxVolume = 0.01f;
        }
        else
        {
            sfxVolume = newVolume;
        }
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }


}
