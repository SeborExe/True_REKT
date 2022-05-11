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
}