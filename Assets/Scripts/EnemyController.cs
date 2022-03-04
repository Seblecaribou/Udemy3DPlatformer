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
    public float waitAtPatrolPoint = 2f;
    public float patrolCounter;
    public float detectionRange = 3f;
    public float attackRange = 1f;

    public Animator enemyAnimator;
    public float runAnimationSpeed = 1f;
    public float runSpeed = 3f;
    public float chaseAnimationSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackAnimationSpeed = 1f;
    public float nextAttackDelay = 1.5f;
    public float attackCounter;

    public enum AIState
    {
        isIdling,
        isPatrolling,
        isChasing,
        isAttacking
    }
    public AIState currentState;
    #endregion

    #region Awake
    private void Awake() { }
    #endregion

    #region Start and Update
    void Start()
    {
        patrolCounter = waitAtPatrolPoint;
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.isIdling:
                RunHandler(false, runAnimationSpeed, runSpeed);
                if (patrolCounter > 0)
                {
                    CountDown(ref patrolCounter);
                } else
                {
                    SetEnemyTarget(patrolPoints[currentPatrolPointIndex].position);
                    StateHandler(AIState.isPatrolling);
                }
                if (PlayerDetector(detectionRange)) StateHandler(AIState.isChasing);
                break;

            case AIState.isPatrolling:
                RunHandler(true, runAnimationSpeed, runSpeed);
                if (agent.remainingDistance <= .2f)
                {
                    ChangeCurrentPatrolPoint();
                    StateHandler(AIState.isIdling);
                    ResetCounter(ref patrolCounter, waitAtPatrolPoint);
                }
                if (PlayerDetector(detectionRange)) StateHandler(AIState.isChasing);
                break;

            case AIState.isChasing:
                agent.speed = 4f;
                RunHandler(true, chaseAnimationSpeed, chaseSpeed);
                if (!PlayerDetector(detectionRange))
                {
                    StateHandler(AIState.isPatrolling);
                    SetEnemyTarget(patrolPoints[currentPatrolPointIndex].position);
                }
                if (PlayerDetector(attackRange))
                {
                    StateHandler(AIState.isAttacking);
                    RunHandler(false, runAnimationSpeed, runSpeed);
                    AttackAnimationHandler();
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                    ResetCounter(ref attackCounter, nextAttackDelay);
                }
                SetEnemyTarget(PlayerController.instance.transform.position);
                break;

            case AIState.isAttacking:
                CountDown(ref attackCounter);
                if (attackCounter <= 0)
                {
                    if (PlayerDetector(attackRange))
                    {
                        AttackAnimationHandler();
                        ResetCounter(ref attackCounter, nextAttackDelay);
                    }
                    if (PlayerDetector(detectionRange) && !PlayerDetector(attackRange)) 
                    { 
                        agent.isStopped = false;
                        SetEnemyTarget(PlayerController.instance.transform.position);
                        StateHandler(AIState.isChasing);
                    } 
                }
                break;
        }
    }
    #endregion

    #region Methods
    private void StateHandler(AIState state)
    {
        currentState = state;
    }

    //TODO: put in lib
    public void CountDown(ref float counter)
    {
        counter -= Time.deltaTime;
    }

    //TODO: put in lib
    public void ResetCounter(ref float counter, float maxCounterValue)
    {
        counter = maxCounterValue;
    }
    
    /// <summary>
    /// Set a NavMeshAgent's target passing the target as argument
    /// </summary>
    /// <param name="newDestination"></param>
    private void SetEnemyTarget(Vector3 newDestination)
    {     
        agent.SetDestination(newDestination);
    }

    private void ChangeCurrentPatrolPoint()
    {
            currentPatrolPointIndex++;
        if (currentPatrolPointIndex == patrolPoints.Length) currentPatrolPointIndex = 0;
    }
    
    private void RunHandler(bool isMoving, float animationSpeed, float agentSpeed)
    { 
        enemyAnimator.speed = animationSpeed;
        enemyAnimator.SetBool("isMoving", isMoving);
        agent.speed = agentSpeed;
    }

    private void AttackAnimationHandler()
    {
        transform.LookAt(PlayerController.instance.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        enemyAnimator.speed = attackAnimationSpeed;
        enemyAnimator.SetTrigger("Attack");
    }
    
    /// <summary>
    /// Evaluate distance between the holder's transform position and the PlayerController's based of a range.
    /// Returns true if player is in range, false if not.
    /// </summary>
    /// <param name="rangeToEvaluate"></param>
    /// <returns></returns>
    private bool PlayerDetector(float rangeToEvaluate)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        return distanceToPlayer > rangeToEvaluate ? false : true;
    }
    #endregion
}
