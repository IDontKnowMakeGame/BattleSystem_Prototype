using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Animation
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        _animator.SetBool(_moveHash, ThisBase.IsMoving);
        if (ThisBase.IsAttack)
        {
            _animator.SetTrigger(_attackHash);
            ThisBase.IsAttack = false;
        }
        if (ThisBase.IsDash)
        {
            _animator.SetTrigger(_dashHash);
            ThisBase.IsDash = false;
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}
