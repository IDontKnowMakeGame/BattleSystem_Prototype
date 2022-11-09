 using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

public class PlayerDash : PlayerMove
{

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
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

    public override void LateUpdate()
    {
        
    }

    protected override void Translate()
    {
        Vector3Int dir = ThisBase.Pos.GamePos + new Vector3Int((int)nextDir.x, 0, (int)nextDir.z);
        if (isMove || ThisBase.IsAttack || !MapManager.NullCheckMap(dir.x, dir.z))
            return;
        switch (nextDir.x)
        {
            case > 0:
                ThisBase.IsRotate = false;
                break;
            case < 0:
                ThisBase.IsRotate = true;
                break;
        }
        switch (nextDir.z)
        {
            case > 0:
                ThisBase.IsForward = true;
                break;
            case < 0:
                ThisBase.IsForward = false;
                break;
        }
        ThisBase.IsDash = true;
        isMove = true;
        nextDir *= 1.5f;
        seq = DOTween.Sequence();
        seq.Append(ThisBase.transform.DOLocalMove(ThisBase.Pos.WorldPos + nextDir, moveSpeed).SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        {
            isMove = false;
            ThisBase.IsDash = false;
            nextDir = Vector3.zero;       
            Vector3Int originalPos = ThisBase.Pos.GamePos;
            ThisBase.Pos.WorldPos = ThisBase.transform.position;
            MapManager.Instance.MoveUnitOn(originalPos ,ThisBase.Pos.GamePos, ThisBase);
            Init();
            seq.Kill();
        });
    }
}