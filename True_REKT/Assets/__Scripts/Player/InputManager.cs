using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationManager animationManager;
    PlayerManager playerManager;
    Animator anim;

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

    private void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
        playerManager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Run.performed += i => runInput = true;
            playerControls.PlayerMovement.Run.canceled += i => runInput = false;
            playerControls.PlayerMovement.QuickTurn.performed += i => quickTurnInput = true;
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
}
