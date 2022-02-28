using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance;

    public AudioSource[] allMusics;
    public AudioSource[] allSFX;
    public AudioMixerGroup musicMixer, sfxMixer, masterMixer;

    public int levelMusic = 0;
    #endregion

    #region Awake
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Start and Update
    void Start()
    {
        PlayMusic(false, levelMusic);
    }

    void Update()
    {
        
    }
    #endregion

    #region Methods
    public void PlayMusic(bool onTop, int trackIndex)
    {
        if (!onTop)
        {
            foreach (AudioSource music in allMusics) music.Stop();
        }
        allMusics[trackIndex].Play();
    }

    public void PlaySFX(bool onTop, int sfxIndex)
    {
        if (!onTop)
        {
            foreach (AudioSource sfx in allSFX) sfx.Stop();
        }
        allSFX[sfxIndex].Play();
    }

    public void SetMasterLevel()
    {
        masterMixer.audioMixer.SetFloat("MasterVolume", UIManager.instance.masterSlider.value);
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVolume", UIManager.instance.sfxSlider.value);
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", UIManager.instance.musicSlider.value);
    }
    #endregion
}
