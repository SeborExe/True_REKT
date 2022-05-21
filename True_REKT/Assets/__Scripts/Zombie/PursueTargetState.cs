using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        if (zombieManager.isPerformingAction)
        {
            RotateTowardTargetWhileAttacking(zombieManager);
            zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        //LOGIC to follow player
        MoveTowardsCurrentTarget(zombieManager);
        RotateTowardsTarget(zombieManager);

        if (zombieManager.distanceFromCurrentTarget <= zombieManager.maximumAttackDistance)
        {
            zombieManager.zombieNavMeshAgent.enabled = false;
            return attackState;
        }
        else
        {
            return this;
        }
    }

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        zombieManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
    }

    //This rotation method use nav mesh agent
    private void RotateTowardsTarget(ZombieManager zombieManager)
    {
        if (zombieManager.canRotate)
        {
            zombieManager.zombieNavMeshAgent.enabled = true;
            zombieManager.zombieNavMeshAgent.SetDestination(zombieManager.currentTarget.transform.position);
            zombieManager.transform.rotation = Quaternion.Slerp(
                zombieManager.transform.rotation,
                zombieManager.zombieNavMeshAgent.transform.rotation,
                zombieManager.rotationSpeed / Time.deltaTime);
        }
    }

    private void RotateTowardTargetWhileAttacking(ZombieManager zombieManager)
    {
        if (zombieManager.canRotate)
        {
            Vector3 direction = zombieManager.currentTarget.transform.position - zombieManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = zombieManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation, targetRotation, zombieManager.rotationSpeed * Time.deltaTime);
        }
    }
}
