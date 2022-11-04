using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : PlayerBehaviour
{
    public PlayerBase ThisBase { get; set; }
    private Animator _animator = null;

    private readonly int _moveHash = Animator.StringToHash("Move");
    private readonly int _dashHash = Animator.StringToHash("Dash");
    private readonly int _attackHash = Animator.StringToHash("Attack");

    private bool isRunningAttackAnime = false;
    public void Awake()
    {
        _animator = ThisBase.GetComponentInChildren<Animator>();
    }

    public void Start()
    {

        
    }

    public void Update()
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

    public void LateUpdate()
    {

    }
}
