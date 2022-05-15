using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationManager animationManager;
    PlayerManager playerManager;
    Animator anim;
    PlayerUIManager playerUIManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    Vector2 movementInput;

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    Vector2 cameraInput;

    [Header("Button inputs")]
    public bool runInput;
    public bool quickTurnInput;
    public bool aimingInput;
    public bool shootInput;
    public bool reloadInput;
    public bool interactionInput;

    private void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
        playerManager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            //Player Movement Inputs
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Run.performed += i => runInput = true;
            playerControls.PlayerMovement.Run.canceled += i => runInput = false;
            playerControls.PlayerMovement.QuickTurn.performed += i => quickTurnInput = true;

            //Player Action Inputs
            playerControls.PlayerActions.Aim.performed += i => aimingInput = true;
            playerControls.PlayerActions.Aim.canceled += i => aimingInput = false; 
            playerControls.PlayerActions.Shoot.performed += i => shootInput = true;
            playerControls.PlayerActions.Shoot.canceled += i => shootInput = false;
            playerControls.PlayerActions.Reload.performed += i => reloadInput = true;
            playerControls.PlayerActions.Interact.performed += i => interactionInput = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
        HandleQuickTurnInput();
        HandleAimInput();
        HandleShootingInput();
        HandleReloadInput();
        HandleInteractionInput();
    }

    private void HandleMovementInput()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);

        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            animationManager.rightHandIK.weight = 0;
            animationManager.leftHandIK.weight = 0;
        }
        else
        {
            animationManager.rightHandIK.weight = 1;
            animationManager.leftHandIK.weight = 1;
        }
    }

    private void HandleCameraInput()
    {
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = cameraInput.y;
    }

    private void HandleQuickTurnInput()
    {
        if (playerManager.isPerformingAction) return;

        if (quickTurnInput)
        {
            anim.SetBool("isPerformingQuickTurn", true);
            animationManager.PlayAnimationWithOutRootMotion("Quick Turn", true);
        }
    }

    private void HandleAimInput()
    {
        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            aimingInput = false;
            anim.SetBool("isAiming", false);
            playerUIManager.crosshair.SetActive(false);
            return;
        }

        if (aimingInput)
        {
            anim.SetBool("isAiming", true);
            playerUIManager.crosshair.SetActive(true);
        }
        else
        {
            anim.SetBool("isAiming", false);
            playerUIManager.crosshair.SetActive(false);
        }

        animationManager.UpdateAimConstrains();
    }

    private void HandleShootingInput()
    {
        //Decide if auto or semi-auto
        if (shootInput && aimingInput)
        {
            shootInput = false;
            playerManager.UseCurrentWeapon();
        }
    }

    private void HandleReloadInput()
    {
        if (playerManager.isPerformingAction) return;

        if (reloadInput)
        {
            reloadInput = false;

            //Check if weapon is curently full
            if (playerManager.playerEquipmentManager.weapon.remainingAmmo == playerManager.playerEquipmentManager.weapon.maxAmmo) return;

            //Check if we have corect ammo type to reload our weapon
            if (playerManager.playerInventoryManager.currentAmmoInInventory != null)
            {
                if (playerManager.playerInventoryManager.currentAmmoInInventory.ammoType == playerManager.playerEquipmentManager.weapon.ammoType)
                {
                    if (playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining == 0) return;


                    int amoutOfAmmoToReload;
                    amoutOfAmmoToReload = playerManager.playerEquipmentManager.weapon.maxAmmo - playerManager.playerEquipmentManager.weapon.remainingAmmo;

                    //Situation when we have more ammo than we need to full reload gun.
                    if (playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining >= amoutOfAmmoToReload)
                    {
                        playerManager.playerEquipmentManager.weapon.remainingAmmo += amoutOfAmmoToReload;
                        playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining =
                            playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining - amoutOfAmmoToReload;
                    }
                    //Situation when we have less ammo than we need to full reload gun.
                    else
                    {
                        playerManager.playerEquipmentManager.weapon.remainingAmmo = playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining;
                        playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining = 0;
                    }

                    playerManager.animationManager.ClearHandIKWeights();
                    playerManager.animationManager.PlayAnimation("Reloading", true);

                    //Replace in future when equipment were finished.
                    playerManager.PlayClip(1);
                    playerManager.playerUIManager.currentAmmoCountText.text = playerManager.playerEquipmentManager.weapon.remainingAmmo.ToString();
                    playerManager.playerUIManager.reserveAmmoCountText.text = playerManager.playerInventoryManager.currentAmmoInInventory.ammoRemaining.ToString();
                }
            }
        }
    }

    private void HandleInteractionInput()
    {
        if (interactionInput)
        {
            if (!playerManager.canInteract)
            {
                interactionInput = false;
            }
        }
    }
}
