using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum StateEnum
{
    Idle, 
    Move,
}

[System.Serializable]
public class AIState
{
    public StateEnum thisState;
    public List<AITransition> transitions = new List<AITransition>();
    public bool isLooping;
    public virtual void DoAction(AIBase thisBase, Action callback)
    {
        
    }
}
