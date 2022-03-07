using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    #region Variables
    public Animator starAnimator;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            starAnimator.SetTrigger("ReachEnd");
        }
    }
    #endregion
}
