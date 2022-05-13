using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] State currentState;

    public IdleState startingState;
    public PlayerManager currentTarget;
    public Rigidbody zombieRigidbody;

    [Header("Flags")]
    public bool isPerformingAction;

    [Header("Typical dynamic")]
    public Animator animator;
    public NavMeshAgent zombieNavMeshAgent;

    [Header("Locomotions")]
    public float rotationSpeed = 5f;
    public float distanceFromCurrentTarget;

    [Header("Attack")]
    public float minimumAttackDistance = 1f;

    private void Awake()
    {
        currentState = startingState;
        zombieNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void Update()
    {
        zombieNavMeshAgent.transform.localPosition = Vector3.zero;

        if (currentTarget != null)
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }

    private void HandleStateMachine()
    {
        State nextState;

        if (currentState != null)
        {
            nextState = currentState.Tick(this);

            if (nextState != null) currentState = nextState;
        }
    }
}
