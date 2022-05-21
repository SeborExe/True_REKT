using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGrappleCollider : MonoBehaviour
{
    ZombieManager zombie;

    public Collider grappleCollider;

    public bool isRightHandedGrappleCollider;

    private void Awake()
    {
        zombie = GetComponentInParent<ZombieManager>();
        grappleCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerManager player = other.GetComponent<PlayerManager>();

            if (player != null)
            {
                if (!player.isPerformingAction)
                {
                    zombie.zombieAnimatorManager.PlayGrappleAnimation("Zombie_Grapple_01", true);
                    zombie.animator.SetFloat("Vertical", 0);

                    player.playerAnimationManager.PlayAnimation("Player_Grapple_01", true);

                    //Make zombie face to his victiom
                    Quaternion targetZombieRotation = Quaternion.LookRotation(player.transform.position - zombie.transform.position);
                    zombie.transform.rotation = targetZombieRotation;

                    //Make player face to zombie
                    Quaternion targetPlayerRotation = Quaternion.LookRotation(zombie.transform.position - player.transform.position);
                    player.transform.rotation = targetPlayerRotation;

                    //In future play different grapple animation depending on angle
                    //If zombie is behind player can't defend himself
                }
            }
        }
    }
}
