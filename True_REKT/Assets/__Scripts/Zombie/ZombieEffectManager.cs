using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEffectManager : MonoBehaviour
{
    ZombieManager zombie;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
    }

    public void DamageZombieHead(int damage)
    {
        //We always stagger for a head shoot
        //Play prooper animation depending on where zombie is shot from
        //Play blood FX
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Head_Forward_01", 0.2f);
        zombie.zombieStatsManager.DealHeadShootDamage(damage);
    }

    public void DamageZombieTorso(int damage)
    {
        //Stagger depending on gun power
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
        zombie.zombieStatsManager.DealTorsoDamage(damage);
    }

    public void DamageZombieLeftArm(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
        zombie.zombieStatsManager.DealArmDamage(true, damage);
    }

    public void DamageZombieRightArm(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
        zombie.zombieStatsManager.DealArmDamage(false, damage);
    }

    public void DamageZombieLeftLeg(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
        zombie.zombieStatsManager.DealLegDamage(true, damage);
    }

    public void DamageZombieRightLeg(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
        zombie.zombieStatsManager.DealLegDamage(false, damage);
    }
}
