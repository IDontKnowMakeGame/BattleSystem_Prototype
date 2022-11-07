using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : Attack
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
       if(isAttacking)
           return;
       ThisBase.StartCoroutine(AttackCoroutine(direction));
    }
    
    private IEnumerator AttackCoroutine(Vector3Int direction)
    {
        isAttacking = true;
        
        float beforeDelay = ((PlayerBase)ThisBase).stat.beforeDelay;
        float afterDelay = ((PlayerBase)ThisBase).stat.afterDelay;
        
        yield return new WaitForSeconds(beforeDelay);
        ThisBase.IsAttack = true;
        var atkPos = ThisBase.Pos.GamePos + direction;
        var atkCube = MapManager.GetCube(atkPos);
        TargetBase = atkCube?.TheUnitOn;
        ((EnemyBase)TargetBase)?.stat.Damage(((PlayerBase)ThisBase).stat.damage);
        Debug.Log("Player Attack" + atkPos);
        yield return new WaitForSeconds(afterDelay);
        
        isAttacking = false;    
        ThisBase.IsAttack = false;
    }
}
