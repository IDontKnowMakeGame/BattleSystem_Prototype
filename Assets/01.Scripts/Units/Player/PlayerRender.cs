using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : PlayerBehaviour
{
    
    public PlayerBase ThisBase { get; set; }

    public void Awake()
    {
        
    }

    public void Start()
    {
        
    }

    public void Update()
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

    public void LateUpdate()
    {
        
    }
}
