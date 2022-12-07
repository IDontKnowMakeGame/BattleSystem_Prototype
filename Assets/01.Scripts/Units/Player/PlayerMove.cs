using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : Move
{
    private PlayerBase ThisPlayer;

    private Queue<Vector3> moveDir = new Queue<Vector3>();

    public bool isSkill = false;

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
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (!isSkill) moveDir.Clear();
            isSkill = !isSkill;
        }
        InputMovement(isSkill ? 2 : 1);
        PopMove();
    }

    public void InputMovement(int speed = 1)
    {
        if (moveDir.Count > 2) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDir.Enqueue(Vector3.forward * speed);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDir.Enqueue(Vector3.back * speed);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir.Enqueue(Vector3.left * speed);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir.Enqueue(Vector3.right * speed);
    }

    public void PopMove()
    {
        if(moveDir.Count > 0 && !isMove)
        {
            nextDir = moveDir.Dequeue();
            Translate();
        }
    }

	public void MovePlayer(Vector3 vec)
	{
		nextDir = vec;
		Translate();
	}

	public override void LateUpdate()
    {
        
    }

    protected virtual void Translate()
    {
        var dir = ThisBase.Pos.GamePos + new Vector3Int((int)nextDir.x, 0, (int)nextDir.z);
        if (isMove || ThisBase.IsAttack)
            return;
        if(!MapManager.NullCheckMap(dir.x, dir.z))
        {
            if (nextDir.magnitude == 2)
            {
                nextDir.Normalize();
                dir = ThisBase.Pos.GamePos + new Vector3Int((int)nextDir.x , 0, (int)nextDir.z);
                if (!MapManager.NullCheckMap(dir.x, dir.z)) return;
            }
            else
                return;
        }
        else
        {
            dir = ThisBase.Pos.GamePos + new Vector3Int((int)nextDir.normalized.x, 0, (int)nextDir.normalized.z);
            if (!MapManager.NullCheckMap(dir.x, dir.z)) return;
        }

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
