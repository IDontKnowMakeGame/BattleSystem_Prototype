using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum ConditionEnum
{
    Time,
}

[System.Serializable]
public class Condition
{
    public ConditionEnum thisCondition;
    public int param;
}

public interface AICondition
{
    public bool CheckCondition(AIBase thisBase);
    public void SetParam(int param);
}
