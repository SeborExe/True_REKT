using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    WeaponLoaderSlot weaponLoaderSlot;
    PlayerManager playerManager;

    [Header("Current equipment")]
    public WeaponItem weapon;
    public WeaponAnimatorManager weaponAnimator;
    //public SubWeaponItem subWeapon;
    RightHandIKTarget rightHandIK;
    LeftHandIKTarget leftHandIK;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
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
        playerManager.playerAnimationManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
        weaponAnimator = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        playerManager.playerAnimationManager.AssignHandIK(rightHandIK, leftHandIK);
        playerManager.playerUIManager.currentAmmoCountText.text = weapon.remainingAmmo.ToString();

        if (playerManager.playerInventoryManager.currentAmmoInInventory != null)
        {
            if (playerManager.playerInventoryManager.currentAmmoInInventory.ammoType == weapon.ammoType)
            {
                playerManager.playerUIManager.reserveAmmoCountText.text = playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining.ToString();
            }
        }
    }
}
