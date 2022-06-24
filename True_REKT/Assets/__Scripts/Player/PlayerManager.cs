using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    AudioSource audioSource;
    Animator anim;
    
    public PlayerUIManager playerUIManager;

    [SerializeField] AudioClip[] audioClips;

    [Header("Typical dynamic")]
    public InputManager inputManager;
    public PlayerLocomotionManager playerLocomotionManager;
    public PlayerCamera playerCamera;
    public PlayerAnimationManager playerAnimationManager;
    public PlayerEquipmentManager playerEquipmentManager;
    public PlayerInventoryManager playerInventoryManager;
    public PlayerStatsManager playerStatsManager;

    [Header("Flags")]
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;
    public bool disableRootMotion;
    public bool isAiming;
    public bool canInteract;

    [Header("Status")]
    public bool isDead;

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        inputManager = GetComponent<InputManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        anim = GetComponent<Animator>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        audioSource = GetComponent<AudioSource>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        isPerformingAction = anim.GetBool("isPerformingAction");
        isPerformingQuickTurn = anim.GetBool("isPerformingQuickTurn");
        disableRootMotion = anim.GetBool("disableRootMotion");
        isAiming = anim.GetBool("isAiming");
        anim.SetBool("isDead", isDead);
    }

    private void FixedUpdate()
    {
        playerLocomotionManager.HandleAllLocomotion();
    }

    private void LateUpdate()
    {
        playerCamera.HandleAllCameraMovement();
    }

    public void UseCurrentWeapon()
    {
        if (isPerformingAction) return;

        if (playerEquipmentManager.weapon.remainingAmmo > 0)
        {
            playerAnimationManager.ChangeColliderHeightWhenActionStart();
            playerEquipmentManager.weapon.remainingAmmo -= 1;
            playerUIManager.currentAmmoCountText.text = playerEquipmentManager.weapon.remainingAmmo.ToString();
            playerAnimationManager.PlayAnimationWithOutRootMotion("Pistol_Shoot", true);
            playerEquipmentManager.weaponAnimator.ShootWeapon(playerCamera);
        }
        else
        {
            PlayClip(0);
        }
    }

    public void PlayClip(int clip)
    {
        audioSource.clip = audioClips[clip];
        audioSource.Play();
    }
}
