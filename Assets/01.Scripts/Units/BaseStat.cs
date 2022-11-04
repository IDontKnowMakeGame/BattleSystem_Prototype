using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseStat
{
    [SerializeField]
    protected int _hp = 0;
    [SerializeField]
    protected int _damage = 0;
    [SerializeField]
    protected float _speed = 0;
    [SerializeField]
    protected float _beforeDelay = 0;
    [SerializeField]
    protected float _afterDelay = 0;
}
