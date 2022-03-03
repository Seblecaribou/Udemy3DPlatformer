using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    //Respawn
    private Vector3 respawnPosition;
    public float respawnDelay = 3f;

    //Coins
    public int currentCoinsTotal = 0;
    public int hurtSound = 8;
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
        CursorHandler(false, CursorLockMode.Locked);
        SetSpawnPoint(PlayerController.instance.transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseUnpause();
    }
    #endregion

    #region Methods
    private void CursorHandler(bool cursorVisible, CursorLockMode lockMode)
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = lockMode;
    }

    public void PauseUnpause()
    {
        UIManager.instance.CloseOptionsMenu();
        if (UIManager.instance.pausePanel.activeInHierarchy)
        {
            UIManager.instance.pausePanel.SetActive(false);
            Time.timeScale = 1;
            CursorHandler(false, CursorLockMode.Locked);
        }
        else
        {
            UIManager.instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            CursorHandler(true, CursorLockMode.None);
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void AddCoin(int coinsToAdd)
    {
        currentCoinsTotal += coinsToAdd;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    public IEnumerator RespawnCoroutine()
    {
        //TODO: refactor the coroutine

        //De-activate player and camera
        
        HealthManager.instance.SetHealthToZero();
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.cinemachineBrain.enabled = false;
        AudioManager.instance.PlaySFX(false, hurtSound);
        UIManager.instance.fadeToBlack = true;
        PlayerController.instance.PlayerExplosionAnimation();

        //Set timeout
        yield return new WaitForSeconds(respawnDelay);

        //Respawn and activate player and camera back in place
        HealthManager.instance.SetHealthToMax();
        UIManager.instance.healthFlowerImage.enabled = true;
        PlayerController.instance.PlayerHealingAnimation(false);
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.gameObject.transform.position = respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
        CameraController.instance.cinemachineBrain.enabled = true;
    }
    #endregion
}
