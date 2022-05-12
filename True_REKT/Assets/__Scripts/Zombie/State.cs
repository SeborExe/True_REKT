using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    //Base class for all future states
    public virtual State Tick()
    {
        Debug.Log("Running State");
        return this;
    }
}
