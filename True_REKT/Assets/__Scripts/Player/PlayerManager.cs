using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;

    AudioSource audioSource;
    Animator anim;
    
    public PlayerUIManager playerUIManager;

    [SerializeField] AudioClip[] audioClips;

    [Header("Typical dynamic")]
    public PlayerLocomotionManager playerLocomotionManager;
    public PlayerCamera playerCamera;
    public AnimationManager animationManager;
    public PlayerEquipmentManager playerEquipmentManager;
    public PlayerInventoryManager playerInventoryManager;

    [Header("Flags")]
    public bool isPerformingAction;
    public bool isPerformingQuickTurn;
    public bool disableRootMotion;
    public bool isAiming;

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        inputManager = GetComponent<InputManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        anim = GetComponent<Animator>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        animationManager = GetComponent<AnimationManager>();
        audioSource = GetComponent<AudioSource>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        isPerformingAction = anim.GetBool("isPerformingAction");
        isPerformingQuickTurn = anim.GetBool("isPerformingQuickTurn");
        disableRootMotion = anim.GetBool("disableRootMotion");
        isAiming = anim.GetBool("isAiming");
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
            playerEquipmentManager.weapon.remainingAmmo -= 1;
            playerUIManager.currentAmmoCountText.text = playerEquipmentManager.weapon.remainingAmmo.ToString();
            animationManager.PlayAnimationWithOutRootMotion("Pistol_Shoot", true);
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
