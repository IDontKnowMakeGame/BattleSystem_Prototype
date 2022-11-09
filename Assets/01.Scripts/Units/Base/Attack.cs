
using System.Collections.Generic;
using UnityEngine;

public class Attack : IBehaviour
{
    public UnitBase ThisBase { get; set; }
    public UnitBase TargetBase { get; set; }
    protected bool isAttacking = false;
    
    public virtual void Init()
    {
        
    }
    
    public virtual void Awake()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
       
    }

    protected virtual void DoAttack(Vector3Int direction)
    {
        
    }

    public virtual void LateUpdate()
    {

    }
}