using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerCamera playerCamera;
    InputManager inputManager;
    PlayerLocomotionManager playerLocomotionManager;
    Animator anim;

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
}
