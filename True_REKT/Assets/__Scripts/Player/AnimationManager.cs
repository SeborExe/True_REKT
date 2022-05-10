using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    PlayerLocomotionManager playerLocomotionManager;
    PlayerManager playerManager;
    RigBuilder rigBuilder;

    float snappedHorizontal;
    float snappedVertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerManager = GetComponent<PlayerManager>();
        rigBuilder = GetComponent<RigBuilder>();
    }

    public void PlayAnimationWithOutRootMotion(string targetAnimation, bool isPerformingAction)
    {
        animator.SetBool("isPerformingAction", isPerformingAction);
        animator.SetBool("disableRootMotion", true);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void HandleAnimatorValues(float horizontalMovement, float verticalMovement, bool isRunning)
    {
        #region snapping values
        if (horizontalMovement > 0)
            snappedHorizontal = 1;

        else if (horizontalMovement < 0)
            snappedHorizontal = -1;

        else
            snappedHorizontal = 0;

        if (verticalMovement > 0)
            snappedVertical = 1;

        else if (verticalMovement < 0)
            snappedVertical = -1;

        else
            snappedVertical = 0;
        #endregion

        if (isRunning && snappedVertical > 0)
        {
            snappedVertical = 2;
        }

        animator.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);
    }

    public void AssignHandIK(RightHandIKTarget rightTarget, LeftHandIKTarget leftTarget)
    {
        rightHandIK.data.target = rightTarget.transform;
        leftHandIK.data.target = leftTarget.transform;
        rigBuilder.Build();
    }

    private void OnAnimatorMove()
    {
        if (playerManager.disableRootMotion) return;

        Vector3 animatorDeltaPosition = animator.deltaPosition;
        animatorDeltaPosition.y = 0;

        Vector3 velocity = animatorDeltaPosition / Time.deltaTime;
        playerLocomotionManager.playerRigidbody.drag = 0;
        playerLocomotionManager.playerRigidbody.velocity = velocity;
        transform.rotation *= animator.deltaRotation;
    }
}
