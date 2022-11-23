using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public enum BehaviourEnum
{
    Move,
    Dash,
    Render,
    Animation,
    Attack,
    Skill
}

public interface IBehaviour
{
    UnitBase ThisBase { get; set; }
    public void Init();
    public void Awake();
    public void Start();
    public void Update();
    public void LateUpdate();
}