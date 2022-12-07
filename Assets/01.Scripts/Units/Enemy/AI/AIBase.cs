using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public EnemyBase EnemyBase;
    public StateEnum CurrentState;
    public List<AIState> states = new();
    private List<AITransition> thisTransitions = new();
    private Dictionary<StateEnum, AIState> stateDict = new Dictionary<StateEnum, AIState>();
    public bool isCurrentStateOver = true;
    public bool canDoAgain = true; 

    private void Awake()
    {
        EnemyBase = GetComponent<EnemyBase>();
        foreach (var state in states)
        {
            var type = System.Type.GetType(state.thisState.ToString() + "State");
            object obj = System.Activator.CreateInstance(type);
            var _state = obj as AIState;
            foreach (var transition in state.transitions)
            {
                transition.InitCondition();
            }

            _state.transitions = state.transitions;
            _state.isLooping = state.isLooping;
            stateDict.Add(state.thisState, _state);
        }
    }

    private void Update()
    {
        thisTransitions = stateDict[CurrentState].transitions;
        if (canDoAgain)
        {
            isCurrentStateOver = false;
            stateDict[CurrentState].DoAction(this, StateOver);
            if (stateDict[CurrentState].isLooping is false)
            {   
                canDoAgain = false;
            }
        }

        if (isCurrentStateOver is true)
        {
            foreach (var transition in thisTransitions)
            {
                bool isAllTrue;
                bool isPositive = transition.IsPositiveAllTrue;
                foreach (var condition in transition.positiveConditions)
                {
                    if (transition.IsPositiveAllTrue)
                    {
                        isPositive &= condition.CheckCondition(this);
                    }
                    else
                    {
                        isPositive |= condition.CheckCondition(this);
                    }
                }

                bool isNegative = transition.IsNegativeAllTrue;
                foreach (var condition in transition.negativeConditions)
                {
                    if (transition.IsNegativeAllTrue)
                    {
                        isNegative &= !condition.CheckCondition(this);
                    }
                    else
                    {
                        isNegative |= !condition.CheckCondition(this);
                    }
                }

                if (transition.IsAllTrue)
                {
                    isAllTrue = isPositive && isNegative;
                }
                else
                {
                    isAllTrue = isPositive || isNegative;
                }

                if (isAllTrue)
                {
                    CurrentState = transition.NextState;
                    canDoAgain = true;
                }
            }
        }
    }

    private void StateOver()
    {
        isCurrentStateOver = true;
    }
}
