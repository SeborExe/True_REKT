using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationBool : StateMachineBehaviour
{
    [Header("isPerformingAction Bool")]
    public string isPerformingAction = "isPerformingAction";
    public bool isPerformingActionStatus = false;

    [Header("isPerformingQuickTurn Bool")]
    public string isPerformingQuickTurn = "isPerformingQuickTurn";
    public bool isPerformingQuickTurnStatus = false;

    [Header("disableRootMotion Bool")]
    public string disableRootMotion = "disableRootMotion";
    public bool disableRootMotionStatus = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isPerformingAction, isPerformingActionStatus);
        animator.SetBool(isPerformingQuickTurn, isPerformingQuickTurnStatus);
        animator.SetBool(disableRootMotion, disableRootMotionStatus);
    }
}
