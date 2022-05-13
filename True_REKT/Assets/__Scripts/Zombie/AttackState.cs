using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    PursueTargetState pursueTargetState;

    [Header("Zombie Attacks")]
    public ZombieAttackAction[] zombieAttackActions;

    [Header("Potential atacks perfomable right now")]
    public List<ZombieAttackAction> potentialAttacks;

    [Header("Current attack performing right now")]
    public ZombieAttackAction currentAttack;

    [Header("State flags")]
    public bool hasPerformedAttack;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        if (zombieManager.isPerformingAction)
        {
            zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        if (!hasPerformedAttack && zombieManager.attackCooldownTimer <= 0)
        {
            if (currentAttack == null)
            {
                //Get a new attack based on distance and angle to player
                GetNewAttack(zombieManager);
            }
            else
            {
                //atack the current target
                Attacktarget(zombieManager);
            }
        }

        if (hasPerformedAttack)
        {
            ResetStateFlags();
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }

    private void GetNewAttack(ZombieManager zombieManager)
    {
        for (int i = 0; i < zombieAttackActions.Length; i++)
        {
            ZombieAttackAction zombieAttack = zombieAttackActions[i];

            //Check for attack distances needed to the perform potential attack
            if (zombieManager.distanceFromCurrentTarget <= zombieAttack.maximumAttackDistance &&
                zombieManager.distanceFromCurrentTarget >= zombieAttack.minimumAttackDistance)
            {
                //Check for attack angles needed to the perfom potential attack
                if (zombieManager.wiewableAngleFromCurrentTarget <= zombieAttack.maximumAttackAngle &&
                    zombieManager.wiewableAngleFromCurrentTarget >= zombieAttack.minimumAttackAngle)
                {
                    potentialAttacks.Add(zombieAttack);
                }
            }
        }

        int randomValue = Random.Range(0, potentialAttacks.Count);

        if (potentialAttacks.Count > 0)
        {
            currentAttack = potentialAttacks[randomValue];
            potentialAttacks.Clear();
        }
    }

    private void Attacktarget(ZombieManager zombieManager)
    {
        if (currentAttack != null)
        {
            hasPerformedAttack = true;
            zombieManager.attackCooldownTimer = currentAttack.attackCooldown;
            zombieManager.zombieAnimatorManager.PlayTargetAttackAnimation(currentAttack.attackAnimation);
        }
        else
        {
            Debug.LogWarning("Zombie is attempting to attack but has no attack");
        }
    }

    private void ResetStateFlags()
    {
        hasPerformedAttack = false;
    }
}
