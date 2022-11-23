using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : Attack
{
    private PlayerBase ThisPlayer = null;
    public override void Awake()
    {
        base.Awake();
        ThisPlayer = ThisBase as PlayerBase;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            DoAttack(Vector3Int.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DoAttack(Vector3Int.back);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DoAttack(Vector3Int.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DoAttack(Vector3Int.right);
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void DoAttack(Vector3Int direction)
    {
       if(isAttacking || ThisBase.IsMoving || ThisBase.IsDash || ((PlayerBase)ThisBase).isSkill)
           return;
       ThisBase.StartCoroutine(AttackCoroutine(direction));
    }
    
    private IEnumerator AttackCoroutine(Vector3Int direction)
    {
        isAttacking = true;
        ThisBase.IsAttack = true;
        
        float beforeDelay = ThisPlayer.stat.GetStat().beforeDelay;
        float afterDelay = ThisPlayer.stat.GetStat().afterDelay;
        
        yield return new WaitForSeconds(beforeDelay);
        ThisBase.isAttack = true;
        var atkPos = ThisBase.Pos.GamePos + direction;
        var atkCube = MapManager.GetCube(atkPos);
        TargetBase = atkCube?.TheUnitOn;
        ((EnemyBase)TargetBase)?.stat.TakeDamage(ThisPlayer.stat.GetStat().damage, 1);    
        if(TargetBase is null)
            ThisPlayer.stat.ReduceAdrenaline(5);
        else
            ThisPlayer.stat.AddAdrenaline(10);
            
        yield return new WaitForSeconds(afterDelay);
        
        isAttacking = false;    
        ThisBase.isAttack = false; 
        ThisBase.IsAttack = false;
    }
}
