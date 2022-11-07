using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class AITransition
{
    public List<AICondition> positiveConditions = new();
    public List<AICondition> negativeConditions = new();
    public List<Condition> _positiveConditions = new();
    public List<Condition> _negativeConditions = new();
    public bool IsAllTrue;
    public bool IsPositiveAllTrue;
    public bool IsNegativeAllTrue;
    public StateEnum NextState;

    public void InitCondition()
    {
        foreach (var condition in _positiveConditions)
        {
            var type = System.Type.GetType(condition.thisCondition.ToString() + "Condition");
            object obj = System.Activator.CreateInstance(type);
            var _condition = obj as AICondition;
            _condition.SetParam(condition.param);
            positiveConditions.Add(_condition);
        }
        
        foreach (var condition in _negativeConditions)
        {
            var type = System.Type.GetType(condition.thisCondition.ToString() + "Condition");
            object obj = System.Activator.CreateInstance(type);
            var _condition = obj as AICondition;   
            _condition.SetParam(condition.param);
            negativeConditions.Add(_condition);
        }
    }
}
