using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PursueTargetState pursueTargetState;

    [Header("Zombie parameters")]
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] float minimumDetectionRadius = -45f;
    [SerializeField] float maximumDetectionRadius = 45f;

    //Idle until find potential target
    //If a target is found we proceed to the pursue target else stay in idle state

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(ZombieManager zombieManager)
    {
        //LOGIC to find a player

        if (zombieManager.currentTarget != null)
        {
            return pursueTargetState;
        }
        else
        {
            FindATargetViaLineOfSight(zombieManager);
            return this;
        }
    }

    private void FindATargetViaLineOfSight(ZombieManager zombieManager)
    {
        //We are searching all colliders with playermanager component
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();

            if (player != null)
            {
                Vector3 targetDiretion = transform.position - player.transform.position;
                float viewableAngle = Vector3.Angle(targetDiretion, transform.forward);

                if (viewableAngle > minimumDetectionRadius && viewableAngle < maximumDetectionRadius)
                {
                    RaycastHit hit;
                    //This just make raycast not start on floor
                    float characterHeight = 1.8f;
                    Vector3 playerStartPosition = new Vector3(player.transform.position.x, characterHeight, player.transform.position.z);
                    Vector3 zombieStartPosition = new Vector3(transform.position.x, characterHeight, transform.position.z);

                    Debug.DrawLine(playerStartPosition, zombieStartPosition, Color.yellow);

                    if (Physics.Linecast(playerStartPosition, zombieStartPosition, out hit))
                    {
                        //Cannot find player
                    }
                    else
                    {
                        zombieManager.currentTarget = player;
                    }
                }
            }
        }
    }
}
