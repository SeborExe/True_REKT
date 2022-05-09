using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    WeaponLoaderSlot weaponLoaderSlot;
    AnimationManager animationManager;

    [Header("Current equipment")]
    public WeaponItem weapon;
    //public SubWeaponItem subWeapon;

    private void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
        LoadWeaponLoaderSlots();
    }

    private void Start()
    {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlots()
    {
        //Back slot
        //Hip slot
        weaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    private void LoadCurrentWeapon()
    {
        weaponLoaderSlot.LoadWeaponModel(weapon);
        animationManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
    }
}
