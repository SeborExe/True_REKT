using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatsManager : MonoBehaviour
{
    ZombieManager zombie;

    [Header("Demage modifiers")]
    public float headShotDamageMultiplier = 1.5f;
    //Maybe add torso and leg,arm modifier that does a little less damage

    [Header("Health stats")]
    public int overallHealth = 100;
    public int headHealth = 100;
    public int torsoHealth = 100;
    public int leftArmHealth = 100;
    public int rightArmHealth = 100;
    public int leftLegHealth = 100;
    public int rightLegHealth = 100;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
    }

    public void DealHeadShootDamage(int damage)
    {
        if (zombie.isDead) return;

        headHealth = headHealth - Mathf.RoundToInt(damage * headShotDamageMultiplier);
        overallHealth = overallHealth - Mathf.RoundToInt(damage * headShotDamageMultiplier);
        CheckForDeath();
    }

    public void DealTorsoDamage(int damage)
    {
        if (zombie.isDead) return;

        torsoHealth -= damage;
        overallHealth -= damage;
        CheckForDeath();
    }

    public void DealArmDamage(bool leftArmDamage, int damage)
    {
        if (zombie.isDead) return;

        if (leftArmDamage)
        {
            leftArmHealth -= damage;
        }
        else
        {
            rightArmHealth -= damage;
        }

        CheckForDeath();
    }

    public void DealLegDamage(bool leftLegDamage, int damage)
    {
        if (zombie.isDead) return;

        if (leftLegDamage)
        {
            leftLegHealth -= damage;
        }
        else
        {
            rightLegHealth -= damage;
        }

        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (overallHealth <= 0)
        {
            overallHealth = 0;
            zombie.isDead = true;
            zombie.zombieAnimatorManager.PlayTargetAnimation("Zombie_Dead_01");
        }
    }
}
