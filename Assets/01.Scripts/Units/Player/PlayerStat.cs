using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerStat : BaseStat
{
    public float rageGauge;
    public float adrenaline;

    public bool IsBerserk = false;
    public WeaponSO weapon;
    public PlayerBase ThisPlayer { get; set; }

    public float _berserkTimer = 0;
    private bool _isBerserkTimerRunning = false;
    
    
    public void Update()
    {
        if (rageGauge <= 0) return;
        if (_isBerserkTimerRunning) return;
        _berserkTimer += Time.deltaTime;
        if (_berserkTimer >= 3)
        {
            _isBerserkTimerRunning = true;
            ThisPlayer.StartCoroutine(ReduceRageGaugeCoroutine());
        }
    }

    public int Damage
    {
        get
        {
            var baseStatDamage = damage;
            var baseWeaponDamage = weapon._weaponStat.damage;
            if (IsBerserk)
                baseWeaponDamage *= 2;
            var temp = baseStatDamage *= baseWeaponDamage;
            return temp;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        rageGauge += 20;
        if(rageGauge >= 100)
        {
            rageGauge = 100;
            IsBerserk = true;
        }
        _isBerserkTimerRunning = false;
        _berserkTimer = 0;
    }
    
    public IEnumerator ReduceRageGaugeCoroutine()
    {
        _berserkTimer = 0;

        while (_isBerserkTimerRunning)
        {
            
            yield return new WaitForSeconds(1f);
            if(_isBerserkTimerRunning is not true) yield break;
            ReduceRageGauge();
        }
    }
    
    public void ReduceRageGauge()
    {
        if (IsBerserk is true)
            rageGauge -= 20;
        else
            rageGauge -= 5;
        if(rageGauge <= 0)
        {
            ResetRageGauge();
        }
    }
    
    public void ResetRageGauge()
    {
        rageGauge = 0;
        IsBerserk = false;
        _isBerserkTimerRunning = false;
    }
}
