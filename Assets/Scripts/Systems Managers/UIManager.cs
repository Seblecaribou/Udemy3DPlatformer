using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public static UIManager instance;

    public GameObject HUD;

    //Black screen
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    //Health UI
    public Text healthText;
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
    }

    public void HideHUD(bool isHidden)
    {
        if (isHidden) HUD.SetActive(false);
        if (!isHidden) HUD.SetActive(true);
    }
    #endregion
}
