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

    public void DamageZombieHead()
    {
        //We always stagger for a head shoot
        //Play prooper animation depending on where zombie is shot from
        //Play blood FX
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Head_Forward_01", 0.2f);
    }

    public void DamageZombieTorso()
    {
        //Stagger depending on gun power
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
    }

    public void DamageZombieLeftArm()
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
    }

    public void DamageZombieRightArm()
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
    }

    public void DamageZombieLeftLeg()
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
    }

    public void DamageZombieRightLeg()
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Torso_Forward_01", 0.2f);
    }
}
