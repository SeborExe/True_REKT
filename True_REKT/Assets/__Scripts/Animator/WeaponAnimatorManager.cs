using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    PlayerManager playerManager;

    [Header("Weapon effects")]
    public GameObject weaponMuzzleFlashFX;
    public GameObject weaponBulletCaseFX;

    [Header("Weapon FX Transforms")]
    public Transform weaponMuzzleFlashTransform;
    public Transform weaponBulletCaseTransform;

    [Header("Weapon Bullet Range")]
    [SerializeField] float bulletRange = 100f;

    [Header("Shootable Layers")]
    public LayerMask shootableLayers;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerManager = GetComponentInParent<PlayerManager>();
    }

    public void ShootWeapon(PlayerCamera playerCamera)
    {
        animator.Play("Shoot");
        audioSource.Play();

        GameObject muzzleFlash = Instantiate(weaponMuzzleFlashFX, weaponMuzzleFlashTransform);
        muzzleFlash.transform.parent = null;

        //GameObject bulletCase = Instantiate(weaponBulletCaseFX, weaponBulletCaseTransform);
        //bulletCase.transform.parent = null;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.cameraObject.transform.position, playerCamera.cameraObject.transform.forward, out hit, 
            bulletRange, shootableLayers))
        {
            ZombieEffectManager zombie = hit.collider.gameObject.GetComponentInParent<ZombieEffectManager>();

            if (zombie != null)
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    zombie.DamageZombieHead(playerManager.playerEquipmentManager.weapon.damage);
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    zombie.DamageZombieTorso(playerManager.playerEquipmentManager.weapon.damage);
                }
                else if (hit.collider.gameObject.layer == 10)
                {
                    zombie.DamageZombieRightArm(playerManager.playerEquipmentManager.weapon.damage);
                }
                else if (hit.collider.gameObject.layer == 11)
                {
                    zombie.DamageZombieLeftArm(playerManager.playerEquipmentManager.weapon.damage);
                }
                else if (hit.collider.gameObject.layer == 12)
                {
                    zombie.DamageZombieRightLeg(playerManager.playerEquipmentManager.weapon.damage);
                }
                else if (hit.collider.gameObject.layer == 13)
                {
                    zombie.DamageZombieLeftLeg(playerManager.playerEquipmentManager.weapon.damage);
                }
            }
        }
    }
}
