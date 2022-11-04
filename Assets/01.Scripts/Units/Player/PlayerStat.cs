using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat : BaseStat
{
    public float RageGauge;
    public float Adrenaline;
    [SerializeField] private WeaponSO _weapon;
}
