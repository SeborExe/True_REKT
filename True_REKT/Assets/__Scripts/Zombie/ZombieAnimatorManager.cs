using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimatorManager : MonoBehaviour
{
    ZombieManager zombie;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
    }

    public void PlayGrappleAnimation(string grappleAnimation, bool useRootMotion)
    {
        zombie.animator.applyRootMotion = useRootMotion;
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade(grappleAnimation, 0.2f);
    }

    public void PlayTargetAttackAnimation(string attackAnimation)
    {
        zombie.animator.applyRootMotion = true;
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade(attackAnimation, 0.2f);
    }

    public void PlayTargetAnimation(string animation)
    {
        zombie.animator.applyRootMotion = true;
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade(animation, 0.2f);
    }
}
