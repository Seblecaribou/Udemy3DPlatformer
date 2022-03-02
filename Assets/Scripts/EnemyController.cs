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
    public float waitAtPoint = 2f;
    public float counter;

    public Animator enemyAnimator;
    public float runAnimationSpeed = 2f;

    public enum AIState
    {
        isIdling,
        isPatrolling
    }
    public AIState currentState;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion

    #region Start and Update
    void Start()
    {
        counter = waitAtPoint;
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.isIdling:
                AnimationHandler(false, runAnimationSpeed);
                if (counter > 0)
                {
                    counter -= Time.deltaTime;
                } else
                {
                    SetPatrolDestination();
                    StateHandler(AIState.isPatrolling);
                } 
                break;

            case AIState.isPatrolling:
                AnimationHandler(true, runAnimationSpeed);
                if (agent.remainingDistance <= .2f)
                {
                    ChangeCurrentPatrolPoint();
                    StateHandler(AIState.isIdling);
                    counter = waitAtPoint;
                }
                break;
        }
    }
    #endregion

    #region Methods

    private void SetPatrolDestination()
    {     
        agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
    }

    private void ChangeCurrentPatrolPoint()
    {
            currentPatrolPointIndex++;
        if (currentPatrolPointIndex == patrolPoints.Length) currentPatrolPointIndex = 0;
    }
    
    private void AnimationHandler(bool isMoving, float animationSpeed)
    { 
        enemyAnimator.speed = animationSpeed;
        enemyAnimator.SetBool("isMoving", isMoving);
    }

    private void StateHandler(AIState state)
    {
        currentState = state;
    }
    #endregion
}
