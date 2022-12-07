using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class StepState : AIState
{
    private Sequence seq;
    public override void DoAction(AIBase thisBase, Action callback)
    {
        seq = DOTween.Sequence();
        var targetPos = Define.PlayerBase.Pos.WorldPos;
        var thisPos = thisBase.EnemyBase.Pos.WorldPos;
        var dir = targetPos - thisPos;
        var pos = new Position
        {
            WorldPos = thisPos + dir
        };
        for (int i = 1; i <= 3; i++)
        {
            
            seq.Append(thisBase.transform.DOMove(thisBase.EnemyBase.Pos.WorldPos + dir * i, 0.5f));
            seq.AppendCallback(() =>
            {
                thisBase.EnemyBase.Pos.MovePos(thisBase.EnemyBase, thisBase.transform.position);
                EnemyAttack.RangeAttack(thisBase.EnemyBase, 1, 50);
            });
            seq.AppendInterval(0.8f);
            seq.AppendCallback(() =>
            {
                pos.WorldPos = thisBase.EnemyBase.Pos.WorldPos + dir;
                if (MapManager.GetCube(pos.GamePos) == null)
                {
                    callback?.Invoke();
                    seq.Kill();
                }
                else if (MapManager.GetCube(pos.GamePos).thisObject == null)
                {
                    callback?.Invoke();
                    seq.Kill();
                }
            });
        }
        seq.AppendCallback(() =>
        {
            callback?.Invoke();
        });
    }
}
