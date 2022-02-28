﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public static UIManager instance;

    public GameObject HUD;
    public GameObject pausePanel;
    public GameObject soundSettingsPanel;

    //Black screen
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    //Health UI
    public Text healthText;
    public Image healthFlowerImage;
    public Sprite[] healthFlowerSpriteSheet;

    //CoinsUI
    public Text coinsCounter;

    //Music Settings UI
    public Slider masterSlider, sfxSlider, musicSlider;
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
        
    }

    void Update()
    {
        FadeToBlack();
        FadeFromBlack();
        UpdateHealthUI();
        UpdateCoinsUI();
    }
    #endregion
    
    #region Methods
    private void FadeToBlack()
    {
        if (fadeToBlack)
        {
            if (blackScreen.color.a == 1f) fadeToBlack = false;
            blackScreen.color = new Color(blackScreen.color.r, 
                                          blackScreen.color.g, 
                                          blackScreen.color.b, 
                                          Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed*Time.deltaTime));
        }
    }

    private void FadeFromBlack()
    {
        if (fadeFromBlack)
        {
            if (blackScreen.color.a == 0f) fadeFromBlack = false;
            blackScreen.color = new Color(blackScreen.color.r,
                                          blackScreen.color.g,
                                          blackScreen.color.b,
                                          Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

        }
    }

    private void UpdateHealthUI()
    {

        healthText.text = HealthManager.instance.currentHealth.ToString();
        switch (HealthManager.instance.currentHealth.ToString())
        {
            case ("5"):
                healthFlowerImage.sprite = healthFlowerSpriteSheet[4];
                break;
            case ("4"):
                healthFlowerImage.sprite = healthFlowerSpriteSheet[3];
                break;
            case ("3"):
                healthFlowerImage.sprite = healthFlowerSpriteSheet[2];

                break;
            case ("2"):
                healthFlowerImage.sprite = healthFlowerSpriteSheet[1];

                break;
            case ("1"):
                healthFlowerImage.sprite = healthFlowerSpriteSheet[0];
                break;
            case ("0"):
                healthFlowerImage.enabled = false;
                break;
        }
    }

    private void UpdateCoinsUI()
    {
        coinsCounter.text = GameManager.instance.currentCoinsTotal.ToString();
    }
    public void HideHUD(bool isHidden)
    {
        if (isHidden) HUD.SetActive(false);
        if (!isHidden) HUD.SetActive(true);
    }
    
    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void LevelSelectMenu()
    {

    }

    public void OpenOptionMenu()
    {
        soundSettingsPanel.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        soundSettingsPanel.SetActive(false);
    }

    public void SetMasterLevel()
    {
        AudioManager.instance.SetMasterLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void MainMenu()
    {

    }
    #endregion
}
