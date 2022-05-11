using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void ShootWeapon()
    {

    }
}
