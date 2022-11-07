using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCondition : AICondition
{
    private bool _isChecking = false;
    private float _time = 0;
    public float _goalTime = 5;
    public bool CheckCondition(AIBase thisBase)
    {
        if (_isChecking == false)
        {
            _time = 0;
            _isChecking = true;
        }
        else
        {
            _time += Time.deltaTime;
            if(_time >= _goalTime)
            {
                _isChecking = false;
                return true;
            }
        }
        return false;
    }

    public void SetParam(int param)
    {
        _goalTime = param;
    }
}
