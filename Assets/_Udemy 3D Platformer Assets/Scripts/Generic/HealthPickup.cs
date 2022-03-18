using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    #region Variables
    public int healthPoints = 1;
    public int healthPickupSound = 7;

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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instance.Heal(healthPoints);
            AudioManager.instance.PlaySFX(true, healthPickupSound);
            Destroy(gameObject);
        }
    }
    #endregion
}
