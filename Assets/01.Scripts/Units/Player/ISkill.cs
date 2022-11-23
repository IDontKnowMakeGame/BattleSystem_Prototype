using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public UnitBase ThisBase { get; set; }
    public void Init();
    public void Awake();
    public void Start();
    public void Update();
    public void LateUpdate();
}
