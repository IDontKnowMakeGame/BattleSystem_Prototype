using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCondition : AICondition
{
    private int _range = 0;
    private Vector3Int _playerPos;
    public bool CheckCondition(AIBase thisBase)
    {
        _playerPos = Define.PlayerBase.Pos.GamePos;
        
        if (Mathf.FloorToInt(Vector3Int.Distance(thisBase.EnemyBase.Pos.GamePos, _playerPos)) <= _range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetParam(int param)
    {
        _range = param;
    }
}
