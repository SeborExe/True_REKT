using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PursueTargetState pursueTargetState;

    //Idle until find potential target
    //If a target is found we proceed to the pursue target else stay in idle state

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    [SerializeField] bool hasTarget;
    public override State Tick()
    {
        //LOGIC to find a player

        if (hasTarget)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
