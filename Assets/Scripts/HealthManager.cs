using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Variables
    public static HealthManager instance;

    public int currentHealth, maxHealth;
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
        
    }
    #endregion

    #region Methods
    public void SetHealthToMax()
    {
        currentHealth = PlayerController.instance.maxHealth;
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        { 
            GameManager.instance.Respawn();
        }
    }

    public void Heal(int healthPoints)
    {
        int sum = currentHealth + healthPoints;
        if (sum <= PlayerController.instance.maxHealth) currentHealth += healthPoints;
    }
    #endregion
}
