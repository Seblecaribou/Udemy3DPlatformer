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
    public float detectionRange = 3f;
    public float attackRange = 1f;

    public Animator enemyAnimator;
    public float runAnimationSpeed = 1f;
    public float chaseAnimationSpeed = 2f;

    public enum AIState
    {
        isIdling,
        isPatrolling,
        isChasing
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
                    SetPatrolDestination(patrolPoints[currentPatrolPointIndex].position);
                    StateHandler(AIState.isPatrolling);
                }
                if (PlayerDetector(detectionRange)) StateHandler(AIState.isChasing);
                break;

            case AIState.isPatrolling:
                AnimationHandler(true, runAnimationSpeed);
                if (agent.remainingDistance <= .2f)
                {
                    ChangeCurrentPatrolPoint();
                    StateHandler(AIState.isIdling);
                    counter = waitAtPoint;
                }
                if (PlayerDetector(detectionRange)) StateHandler(AIState.isChasing);
                break;

            case AIState.isChasing:
                AnimationHandler(true, chaseAnimationSpeed);
                agent.speed = 4f;
                if (!PlayerDetector(detectionRange)) 
                {
                    StateHandler(AIState.isPatrolling);
                    SetPatrolDestination(patrolPoints[currentPatrolPointIndex].position);
                }
                SetPatrolDestination(PlayerController.instance.transform.position);
                break;
        }
    }
    #endregion

    #region Methods

    private void SetPatrolDestination(Vector3 newDestination)
    {     
        agent.SetDestination(newDestination);
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

    private bool PlayerDetector(float rangeToEvaluate)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        return distanceToPlayer > rangeToEvaluate ? false : true;
    }
    #endregion
}
