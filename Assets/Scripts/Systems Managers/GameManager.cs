﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    private Vector3 respawnPosition;

    public float respawnDelay = 3f;

    //Cursor
    public bool cursorVisible = false;
    public CursorLockMode cursorLockMode = CursorLockMode.Locked;
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
        CursorHandler();
        respawnPosition = PlayerController.instance.transform.position;
    }

    void Update()
    {
        
    }
    #endregion

    #region Methods
    private void CursorHandler()
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorLockMode;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    public IEnumerator RespawnCoroutine()
    {
        //De-activate player and camera
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.cinemachineBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;
        PlayerController.instance.PlayerExplosionAnimation();

        //Set timeout
        yield return new WaitForSeconds(respawnDelay);

        //Respawn and activate player and camera back in place
        HealthManager.instance.SetHealthToMax();
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.gameObject.transform.position = respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
        CameraController.instance.cinemachineBrain.enabled = true;
    }
    #endregion
}