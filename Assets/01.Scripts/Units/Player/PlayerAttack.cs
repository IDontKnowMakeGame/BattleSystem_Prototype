using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerBehaviour
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
        if (Input.GetKeyDown(KeyCode.Z) && !ThisBase.IsAttack)
        {
            ThisBase.IsAttack = true;
        }
    }

    public void LateUpdate()
    {
        
    }
}
