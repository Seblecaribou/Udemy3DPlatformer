using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    #region Variables
    public int damage = 1;
    public bool canKnockback = true;
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
        if (other.tag == "Player") HealthManager.instance.DealDamage(damage, canKnockback);
    }
    #endregion
}
