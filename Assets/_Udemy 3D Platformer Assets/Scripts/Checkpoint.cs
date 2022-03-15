using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Variables
    public GameObject checkpointOn;
    public GameObject checkpointOff;
    public int checkpointSound;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion
    
    #region Start and Update
    void Start()
    {
        checkpointOff.SetActive(true);
        checkpointOn.SetActive(false);
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
            //Change spawn point
            GameManager.instance.SetSpawnPoint(transform.position);
            AudioManager.instance.PlaySFX(true, checkpointSound);

            //De-activates all checkpoints before activating the triggered one
            Checkpoint[] allCheckpoints = FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint checkpoint in allCheckpoints)
            {
                checkpoint.checkpointOff.SetActive(true);
                checkpoint.checkpointOn.SetActive(false);
            }

            //Activate the triggered checkpoint
            checkpointOff.SetActive(false);
            checkpointOn.SetActive(true);
        }
    }
    #endregion
}