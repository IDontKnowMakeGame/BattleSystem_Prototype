using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class Move : IBehaviour
{
    protected Sequence seq;
    public UnitBase ThisBase { get; set; }
    protected Vector3 nextDir = Vector3.zero;
    protected bool isMove = false;
    protected float moveSpeed = 0;

    public virtual void Init()
    {
        var player = (PlayerBase)ThisBase;
        var weight = (int)player.stat.weapon._weaponStat.Weight;
        moveSpeed = weight * 0.1f;
    }
    public virtual void Awake()
    {
        Init();
    }
    
    public virtual void Start()
    {
        
    }
    
    public virtual void Update()
    {
        
    }
    
    public virtual void LateUpdate()
    {
        
    }
}
