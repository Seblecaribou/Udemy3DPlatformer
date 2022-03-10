using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Variables
    public string firstLevel, levelSelect;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion

    #region Start and Update
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    #region Methods
    public void NewGame()
    {
        GameManager.instance.MainMenuChecker();
        SceneManager.LoadScene(firstLevel);
    }

    public void ContinueGame()
    {
        GameManager.instance.MainMenuChecker();
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
    #endregion
}
