using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    //Cursor
    public bool cursorVisible = false;
    public CursorLockMode cursorLockMode = CursorLockMode.Locked;
    #endregion

    #region Start and Update
    void Start()
    {
        cursorHandler();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Methods
    private void cursorHandler()
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorLockMode;
    }
    #endregion
}
