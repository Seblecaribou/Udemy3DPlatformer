using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Variables
    public static HealthManager instance;

    //Player health
    public int currentHealth;

    //Invicibility handler
    public float invincibilityLength = 2f;
    public float invincibilityCounter;
    public float flickeringIntensity = 25f;
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
        SetHealthToMax();
    }

    void Update()
    {
        SetInvincibility();
    }
    #endregion

    #region Methods
    public void SetHealthToMax()
    {
        currentHealth = PlayerController.instance.maxHealth;
    }

    public void SetHealthToZero()
    {
        currentHealth = 0;
    }

    public void SetInvincibility()
    {
        if (invincibilityCounter > 0) invincibilityCounter -= Time.deltaTime;
        PlayerController.instance.PlayerFlicker(invincibilityCounter, flickeringIntensity);
    }

    public void DealDamage(int damage, bool canKnockback)
    {
        if (invincibilityCounter > 0) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.instance.Respawn();
        } else
        {
            PlayerController.instance.SetKnockback(canKnockback);
            invincibilityCounter = invincibilityLength;
        }
    }

    public void Heal(int healthPoints)
    { 
        currentHealth += healthPoints;
        if (currentHealth > PlayerController.instance.maxHealth) SetHealthToMax();
        PlayerController.instance.PlayerHealingAnimation(true);
    }
    #endregion
}
