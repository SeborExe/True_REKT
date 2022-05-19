using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatManager : MonoBehaviour
{
    ZombieGrappleCollider rightGrappleCollider;
    ZombieGrappleCollider leftGrappleCollider;

    private void Awake()
    {
        LoadGrappleColliders();
    }

    private void LoadGrappleColliders()
    {
        ZombieGrappleCollider[] grappleColliders = GetComponentsInChildren<ZombieGrappleCollider>();

        foreach (var grappleCollider in grappleColliders)
        {
            if (grappleCollider.isRightHandedGrappleCollider)
            {
                rightGrappleCollider = grappleCollider;
            }
            else
            {
                leftGrappleCollider = grappleCollider;
            }
        }
    }

    public void OpenGrappleCollider()
    {
        rightGrappleCollider.grappleCollider.enabled = true;
        leftGrappleCollider.grappleCollider.enabled = true;
    }

    public void CloseGrappleCollider()
    {
        rightGrappleCollider.grappleCollider.enabled = false;
        leftGrappleCollider.grappleCollider.enabled = false;
    }
}
