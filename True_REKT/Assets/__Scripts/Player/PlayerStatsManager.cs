using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    PlayerManager playerManager;

    [Header("Health")]
    public int health = 100;

    [Header("Handing Damage")]
    public int pendingDamage = 0;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void KillPlayer()
    {
        if (!playerManager.isPerformingAction)
        {
            playerManager.playerAnimationManager.PlayAnimation("Dead_01", true);
        }

        playerManager.isDead = true;
    }

    public void TakeDameFromGrapple()
    {
        health -= pendingDamage;

        if (health <= 0)
        {
            KillPlayer();
        }
        else
        {
            playerManager.playerUIManager.DisplayHealthPopUp();
        }
    }
}
