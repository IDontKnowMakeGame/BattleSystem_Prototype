using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseStat
{
    public int hp = 0;
    public int damage = 0;
    public float speed = 0;
    public float beforeDelay = 0;
    public float afterDelay = 0;
    
    public virtual void TakeDamage(int damage, float reduce)
    {
        hp -= Mathf.RoundToInt(damage * reduce);
        if (hp <= 0)
        {
            hp = 0;
        }
    }
}
