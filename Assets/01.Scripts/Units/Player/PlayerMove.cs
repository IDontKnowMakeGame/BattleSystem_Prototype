using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : Move
{
    private PlayerBase ThisPlayer;

    public override void Awake()
    {
        base.Awake();
        ThisPlayer = ThisBase as PlayerBase;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            nextDir = Vector3.forward;
            Translate();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            nextDir = Vector3.left;
            Translate();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            nextDir = Vector3.back;
            Translate();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            nextDir = Vector3.right;
            Translate();
        }
        
    }

    public override void LateUpdate()
    {
        
    }

    protected virtual void Translate()
    {
        var dir = ThisBase.Pos.GamePos + new Vector3Int((int)nextDir.x, 0, (int)nextDir.z);
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
        ThisBase.IsMoving = true;
        isMove = true;
        nextDir *= 1.5f;
        seq = DOTween.Sequence();
        seq.Append(ThisBase.transform.DOLocalMove(ThisBase.Pos.WorldPos + nextDir, ThisPlayer.stat.GetStat().speed).SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        {
            isMove = false;
            ThisBase.IsMoving = false;
            nextDir = Vector3.zero;
            Vector3Int originalPos = ThisBase.Pos.GamePos;
            ThisBase.Pos.WorldPos = ThisBase.transform.position;
            Vector3Int newPos = ThisBase.Pos.GamePos;
            MapManager.Instance.MoveUnitOn(originalPos ,newPos, ThisBase);
            Init();
            seq.Kill();
        });
    }
}
