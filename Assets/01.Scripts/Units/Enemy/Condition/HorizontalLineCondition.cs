using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalLineCondition : AICondition
{
    private Vector3Int _playerPos;
    private Vector3Int _enemyPos;
    public bool CheckCondition(AIBase thisBase)
    {
        _playerPos = Define.PlayerBase.Pos.GamePos;
        _enemyPos = thisBase.EnemyBase.Pos.GamePos;
        return _playerPos.z == _enemyPos.z;
    }

    public void SetParam(int param)
    {
        
    }
}
