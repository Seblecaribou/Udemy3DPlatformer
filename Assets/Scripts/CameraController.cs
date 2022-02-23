using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    #region Variables
    public static CameraController instance;

    public CinemachineBrain cinemachineBrain;
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

    }
    #endregion

    #region Methods
    #endregion
}
