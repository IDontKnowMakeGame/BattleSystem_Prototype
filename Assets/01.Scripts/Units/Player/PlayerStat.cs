using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerStat : BaseStat
{
    public float reduceDamage => IsBerserk ? 0.5f : 1f;
    public float rageGauge;
    public float adrenaline;

    public bool IsBerserk = false;
    public bool IsMadden = false;
    public WeaponSO weapon;
    public PlayerBase ThisPlayer { get; set; }

	public BasicHPSlider adSlider;
	public BasicHPSlider rageSlider;

	public float _berserkTimer = 0;
    private bool _isBerserkTimerRunning = false;    


    public BaseStat GetStat()
    {
        var stat = new BaseStat();
        stat.hp = this.hp + weapon._weaponStat.hp;
        stat.damage = this.damage;
        if (IsBerserk)
            stat.damage += weapon._weaponStat.damage * 2;
        else
            stat.damage += weapon._weaponStat.damage;
        stat.speed = (int)this.weapon._weaponStat.Weight * 0.1f;
        stat.beforeDelay = this.beforeDelay + weapon._weaponStat.beforeDelay;
        if (IsMadden)
        {
            stat.speed -= 0.1f;
            stat.beforeDelay -= 0.3f;
        }
        stat.afterDelay = this.afterDelay + weapon._weaponStat.afterDelay;
        return stat;
    }
    
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

    public override void TakeDamage(int damage, float reduce = 1f)
    {
        base.TakeDamage(damage, reduceDamage);
        rageGauge += 20;
		rageSlider.SetSlider(rageGauge);
		ReduceAdrenaline(50);

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

		rageSlider.SetSlider(rageGauge);

		if (rageGauge <= 0)
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
    
    public void AddAdrenaline(float amount)
    {
        adrenaline += amount;
		adSlider.SetSlider(adrenaline, true);
        if (adrenaline >= 100)
        {
            adrenaline = 100;
            if (IsMadden is false)
            {
                IsMadden = true;
                ThisPlayer.StartCoroutine(ReduceAdrenalineCoroutine());
            }
            
        }
	}
    
    public void ReduceAdrenaline(float amount)
    {
        adrenaline -= amount;
        if (adrenaline <= 0)
        {
            adrenaline = 0;
            IsMadden = false;   
        }
		adSlider.SetSlider(adrenaline);
	}
    
    public IEnumerator ReduceAdrenalineCoroutine()
    {
        while (IsMadden)
        {
            yield return new WaitForSeconds(0.25f);
            if(IsMadden is not true) yield break;
            ReduceAdrenaline(7.5f);
        }
    }
}
