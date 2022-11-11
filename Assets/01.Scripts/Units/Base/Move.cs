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

    public virtual void Init()
    {
        
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
