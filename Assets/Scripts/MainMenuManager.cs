using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Variables
    public string firstLevel, levelSelect;

    public GameObject continueButton;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion

    #region Start and Update
    void Start()
    {
        if (PlayerPrefs.HasKey("Continue")) continueButton.SetActive(true);
    }

    void Update()
    {
        
    }
    #endregion

    #region Methods
    public void NewGame()
    {
        PlayerPrefs.SetInt("Continue", 0);
        GameManager.instance.MainMenuChecker();
        SceneManager.LoadScene(firstLevel);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToDesktop()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    #endregion
}
