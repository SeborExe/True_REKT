using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Zombie Attack Action")]
public class ZombieAttackAction : ScriptableObject
{
    [Header("Attack Animation")]
    public string attackAnimation;

    [Header("Attack cooldown")]
    public float attackCooldown = 5f;           //Time before the zombie can perform another attack

    [Header("Attack angles and distances")]
    public float maximumAttackAngle = 20f;            //Maximum and minimum angle to perform this attack
    public float minimumAttackAngle = -20f;
    public float maximumAttackDistance = 0.5f;         //maximum anim minimum distance to perform this attack
    public float minimumAttackDistance = 2f;
}
