using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalLineCondition : AICondition
{
    int _line;
    private Vector3Int _playerPos;
    public bool CheckCondition(AIBase thisBase)
    {
        _playerPos = Define.PlayerBase.Pos.GamePos;
        return _playerPos.z == _line;
    }

    public void SetParam(int param)
    {
        _line = param;
    }
}
