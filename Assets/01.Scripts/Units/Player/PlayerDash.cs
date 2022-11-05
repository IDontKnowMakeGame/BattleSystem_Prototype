using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

public class PlayerDash : PlayerMove
{
    private Sequence seq;
    private Vector3 nextDir = Vector3.zero;
    private bool isDash = false;

    public void Awake()
    {
        
    }

    public void Start()
    {
        
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextDir = Vector3.forward;
            Translate();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextDir = Vector3.left;
            Translate();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextDir = Vector3.back;
            Translate();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextDir = Vector3.right;
            Translate();
        }
    }

    public void LateUpdate()
    {
        
    }

    protected override void Translate()
    {
        Vector3Int dir = ThisBase.Pos.GamePos + new Vector3Int((int)nextDir.x, 0, (int)nextDir.z);
        if (isDash || ThisBase.IsAttack || !MapManager.NullCheckMap(dir.x, dir.z))
            return;
        ThisBase.IsDash = true;
        isDash = true;
        nextDir *= 1.5f;
        switch (nextDir.x)
        {
            case > 0:
                ThisBase.IsRotate = false;
                break;
            case < 0:
                ThisBase.IsRotate = true;
                break;
        }
        seq = DOTween.Sequence();
        seq.Append(ThisBase.transform.DOLocalMove(ThisBase.Pos.WorldPos + nextDir, 0.3f).SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        {
            isDash = false;
            ThisBase.IsDash = false;
            nextDir = Vector3.zero;
            ThisBase.Pos.WorldPos = ThisBase.transform.position;
            seq.Kill();
        });
    }
}