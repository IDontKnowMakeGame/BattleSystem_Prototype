using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : UnitBase
{
    public PlayerStat stat = new PlayerStat();

    protected override void Init()
    {
        foreach (var behaviour in behaviours)
        {
            var type = System.Type.GetType("Player"+ behaviour.ToString());
            if (type == null) continue;
            var obj = System.Activator.CreateInstance(type);
            var behave = obj as IBehaviour;
            if (behave == null) continue;
            behave.ThisBase = this;
            _behaviours.Add(behave);
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }
}
