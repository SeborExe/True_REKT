using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    WeaponLoaderSlot weaponLoaderSlot;
    AnimationManager animationManager;

    [Header("Current equipment")]
    public WeaponItem weapon;
    public WeaponAnimatorManager weaponAnimator;
    //public SubWeaponItem subWeapon;
    RightHandIKTarget rightHandIK;
    LeftHandIKTarget leftHandIK;

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
        weaponAnimator = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        animationManager.AssignHandIK(rightHandIK, leftHandIK);
    }
}
