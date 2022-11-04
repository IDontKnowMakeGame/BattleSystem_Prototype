using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

public class PlayerDash : PlayerBehaviour
{
    private Sequence seq;
    public PlayerBase ThisBase { get; set; }
    private Vector3 nextDir = Vector3.zero;
    private bool isDash = false;

    public void Awake()
    {
        
    }

    public void Start()
    {
        
    }

    public void Update()
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

    public void Translate()
    {
        if (isDash || ThisBase.IsAttack)
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