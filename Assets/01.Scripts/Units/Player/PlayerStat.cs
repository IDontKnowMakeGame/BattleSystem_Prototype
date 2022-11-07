using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat : BaseStat
{
    public float rageGauge;
    public float adrenaline;

    private bool IsBerserk => rageGauge >= 100;
    [SerializeField] private WeaponSO weapon;

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
}
