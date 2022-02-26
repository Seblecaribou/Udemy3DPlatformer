using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public static UIManager instance;

    //Black screen
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;
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
    #endregion
}
