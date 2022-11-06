using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : IBehaviour
{
    protected Animator _animator = null;
    protected readonly int _moveHash = Animator.StringToHash("Move");
    protected readonly int _dashHash = Animator.StringToHash("Dash");
    protected readonly int _attackHash = Animator.StringToHash("Attack");
    public UnitBase ThisBase { get; set; }
    public virtual void Awake()
    {
        _animator = ThisBase.GetComponentInChildren<Animator>();
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
