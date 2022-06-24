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
    public ZombieAnimatorManager zombieAnimatorManager;
    public ZombieStatsManager zombieStatsManager;
    public ZombieCombatManager zombieCombatManager;

    [Header("Flags")]
    public bool isPerformingAction;
    public bool isDead;
    public bool canRotate;

    [Header("Typical dynamic")]
    public Animator animator;
    public NavMeshAgent zombieNavMeshAgent;

    [Header("Locomotions")]
    public float rotationSpeed = 5f;
    public float distanceFromCurrentTarget;
    public float wiewableAngleFromCurrentTarget;
    public Vector3 targetsDirections;

    [Header("Attack")]
    public float minimumAttackDistance = 0.5f; //Set this to your minimum distance of shortest zombie attack
    public float maximumAttackDistance = 2f;   //Set this to your maximum distance of longest zombie attack
    public float attackCooldownTimer;

    private void Awake()
    {
        currentState = startingState;
        zombieNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieRigidbody = GetComponent<Rigidbody>();
        zombieAnimatorManager = GetComponent<ZombieAnimatorManager>();
        zombieStatsManager = GetComponent<ZombieStatsManager>();
        zombieCombatManager = GetComponent<ZombieCombatManager>();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            HandleStateMachine();
        }

    }

    private void Update()
    {
        zombieNavMeshAgent.transform.localPosition = Vector3.zero;

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer = attackCooldownTimer - Time.deltaTime;
        }

        if (currentTarget != null)
        {
            targetsDirections = currentTarget.transform.position - transform.position;
            wiewableAngleFromCurrentTarget = Vector3.SignedAngle(targetsDirections, transform.forward, Vector3.up);
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
