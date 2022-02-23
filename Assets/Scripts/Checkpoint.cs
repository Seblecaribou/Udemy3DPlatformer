using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Variables
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
            GameManager.instance.SetSpawnPoint(transform.position);
        }
    }
    #endregion
}
