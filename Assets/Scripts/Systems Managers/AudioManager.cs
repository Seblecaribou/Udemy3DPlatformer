using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public static AudioManager instance;

    public AudioSource[] allMusics;
    public AudioSource[] allSFX;

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
    #endregion
}
