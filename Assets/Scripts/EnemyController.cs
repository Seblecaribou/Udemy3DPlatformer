using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Variables
    public NavMeshAgent agent;

    public Transform[] patrolPoints;
    public int currentPatrolPointIndex;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion

    #region Start and Update
    void Start()
    {
        SetPatrolDestination();
    }

    void Update()
    {
        ChangeCurrentPatrolPoint();
    }
    #endregion

    #region Methods

    private void SetPatrolDestination()
    {     
        agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
    }

    private void ChangeCurrentPatrolPoint()
    {
        if (agent.remainingDistance <= .2f)
        {
            currentPatrolPointIndex++;
            if (currentPatrolPointIndex == patrolPoints.Length) currentPatrolPointIndex = 0;
            SetPatrolDestination();
        }
    }
    #endregion
}
