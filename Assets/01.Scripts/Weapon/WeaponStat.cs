using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponWeight
{
    SuperLight = 2,
    Light = 4,
    Medium = 5,
    Heavy = 7,
    SuperHeavy = 10,
}
[Serializable]
public class WeaponStat : BaseStat
{
    public WeaponWeight Weight;
}
