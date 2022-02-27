﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    #region Variables
    public int coinValue = 1;
    public GameObject shinyEffect;
    #endregion

    #region Awake
    void Awake() { }
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
            GameManager.instance.AddCoin(coinValue);
            Destroy(gameObject);
            GameObject coinEffect = Instantiate(shinyEffect, transform.position, transform.rotation);
            Destroy(coinEffect, 1f);

        }
    }
    #endregion
}
