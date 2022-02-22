using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public bool cursorVisible = false;
    #endregion

    #region Start and Update
    void Start()
    {
        Cursor.visible = cursorVisible;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
