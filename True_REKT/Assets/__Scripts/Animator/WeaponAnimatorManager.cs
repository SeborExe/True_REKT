using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator animator;

    [Header("Weapon effects")]
    public GameObject weaponMuzzleFlashFX;
    public GameObject weaponBulletCaseFX;

    [Header("Weapon FX Transforms")]
    public Transform weaponMuzzleFlashTransform;
    public Transform weaponBulletCaseTransform;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void ShootWeapon(PlayerCamera playerCamera)
    {
        animator.Play("Shoot");

        GameObject muzzleFlash = Instantiate(weaponMuzzleFlashFX, weaponMuzzleFlashTransform);
        muzzleFlash.transform.parent = null;

        //GameObject bulletCase = Instantiate(weaponBulletCaseFX, weaponBulletCaseTransform);
        //bulletCase.transform.parent = null;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.cameraObject.transform.position, playerCamera.cameraObject.transform.forward, out hit))
        {

        }
    }
}
