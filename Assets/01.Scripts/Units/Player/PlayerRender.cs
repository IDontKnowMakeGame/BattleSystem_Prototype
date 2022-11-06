using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : Render
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
        if (ThisBase.IsRotate)
        {
            ThisBase.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            ThisBase.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}
