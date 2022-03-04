using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    #region Variables
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
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().TakeDamage();
            PlayerController.instance.BouncePlayer(1f);
        }
    }
    #endregion
}
