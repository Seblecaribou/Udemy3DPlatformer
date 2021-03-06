using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    #region Variables
    public int damage = 1;
    public bool canKnockback = true;
    public int hurtSound = 8;
    #endregion

    #region Awake
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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instance.DealDamage(damage, canKnockback);
            AudioManager.instance.PlaySFX(true, hurtSound);
        } 
    }
    #endregion
}
