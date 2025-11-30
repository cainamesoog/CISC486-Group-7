using PurrNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : NetworkIdentity
{
    // Variables
    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    public Transform targetWitch;

    public float detectionRadius = 15f;
    public float attackRadius = 3f;
    public float resetDelay = 2f;
    public float attackCooldown = 1.5f;

    private float attackTimer;
    private float resetTimer;
    private int currentPatrolIndex;
    private int patrolDirection = 1;
    private Vector3 initialPosition;
    private Renderer rend;
    private State currentState;

    public enum State
    {
        Patrol,
        Pursuit,
        Attack,
        Reset
    }

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (targetWitch == null)
            targetWitch = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.stoppingDistance = 0.1f;
        agent.autoBraking = false;
        agent.acceleration = 30f;
        agent.angularSpeed = 1200f;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        rend = GetComponent<Renderer>();
        initialPosition = transform.position;
        currentState = State.Patrol;
        GoToNextPatrolPoint();

    }

    // Update is called once per frame
    void Update()
    {

        if (targetWitch == null)
        {
            targetWitch = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        switch (currentState)
        {
            case State.Patrol:
                PatrolState();
                rend.material.color = Color.green;
                break;
            case State.Pursuit:
                PursuitState();
                rend.material.color = Color.red;
                break;
            case State.Attack:
                AttackState();
                rend.material.color = Color.black;
                break;
            case State.Reset:
                ResetState();
                rend.material.color = Color.blue;
                break;
        }
    }

    private void PatrolState()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();

        // Check detection purely via distance
        if (IsTargetInRange(detectionRadius))
        {
            ChangeState(State.Pursuit);
        }
    }

    private void PursuitState()
    {
        if (targetWitch == null)
        {
            ChangeState(State.Reset);
            return;
        }

        agent.SetDestination(targetWitch.position);

        float distance = Vector3.Distance(transform.position, targetWitch.position);
        if (distance <= attackRadius)
        {
            ChangeState(State.Attack);
        }
        else if (distance > (detectionRadius * 1.25f)) // lost aggro
        {
            ChangeState(State.Reset);
        }
    }

    private void AttackState()
    {
        if (targetWitch == null)
        {
            ChangeState(State.Reset);
            return;
        }

        float distance = Vector3.Distance(transform.position, targetWitch.position);
        agent.ResetPath();
        agent.velocity = Vector3.zero;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            Debug.Log("The knight attacks the witch");
            attackTimer = attackCooldown;
        }

        if (distance > attackRadius)
        {
            ChangeState(State.Pursuit);
        }
    }

    private void ResetState()
    {
        if (agent.pathPending)
            return;

        if (agent.remainingDistance > 0.5f)
        {
            agent.SetDestination(initialPosition);
        }
        else
        {
            resetTimer += Time.deltaTime;
            if (resetTimer >= resetDelay)
            {
                resetTimer = 0f;
                ChangeState(State.Patrol);
            }
        }

        if (IsTargetInRange(detectionRadius))
        {
            ChangeState(State.Pursuit);
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        initialPosition = agent.destination;
        currentPatrolIndex += patrolDirection;

        if (currentPatrolIndex >= patrolPoints.Length)
        {
            patrolDirection = -1;
            currentPatrolIndex = patrolPoints.Length - 2;
        }
        else if (currentPatrolIndex < 0)
        {
            patrolDirection = 1;
            currentPatrolIndex = 1;
        }
    }

    private bool IsTargetInRange(float range)
    {
        if (targetWitch == null) return false;
        return Vector3.Distance(transform.position, targetWitch.position) <= range;
    }

    private void ChangeState(State newState)
    {
        if (currentState == newState) return;

        // Debug.Log($"Knight state changed: {currentState} -> {newState}");
        currentState = newState;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
