using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    #region Variables
    public int healthPoints = 1;
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
        if (other.tag == "Player") HealthManager.instance.Heal(healthPoints);
        Destroy(gameObject);
    }
    #endregion
}
