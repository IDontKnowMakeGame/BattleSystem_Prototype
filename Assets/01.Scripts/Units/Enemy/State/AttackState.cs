using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class AttackState : AIState
{
    private Sequence seq;
    public override void DoAction(AIBase thisBase, Action callback)
    {
        seq = DOTween.Sequence();

        for (int i = 1; i <= 4; i++)
        {
            int n = i;
            seq.AppendCallback(() =>
            {
                EnemyAttack.FourWayAttack(thisBase.EnemyBase, n, 80);
            });
            seq.AppendInterval(0.2f);
        }
        
        seq.AppendCallback(() =>
        {
            callback?.Invoke();
        });
    }
}
