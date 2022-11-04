using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : PlayerBehaviour
{
    private Sequence seq;
    public PlayerBase ThisBase { get; set; }
    Vector3 nextDir = Vector3.zero;
    private bool isMoving = false;

    public void Awake()
    {
        
    }

    public void Start()
    {
        
    }

    public void Update()
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

    public void LateUpdate()
    {
        
    }

    public void Translate()
    {
        if (isMoving || ThisBase.IsAttack)
            return;
        ThisBase.IsMoving = true;
        isMoving = true;
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
            isMoving = false;
            ThisBase.IsMoving = false;
            nextDir = Vector3.zero;
            ThisBase.Pos.WorldPos = ThisBase.transform.position;
            seq.Kill();
        });
    }
}
