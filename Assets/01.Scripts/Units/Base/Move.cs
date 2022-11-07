using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Move : IBehaviour
{
    protected Sequence seq;
    public UnitBase ThisBase { get; set; }
    protected Vector3 nextDir = Vector3.zero;
    protected bool isMove = false;

    public virtual void Awake()
    {
        
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
