using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public override void DoAction(AIBase thisBase, Action callback)
    {
        Debug.Log("Current State is Idle");
        callback?.Invoke();
    }
}