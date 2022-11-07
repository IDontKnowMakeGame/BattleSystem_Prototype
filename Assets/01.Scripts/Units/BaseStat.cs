using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseStat
{
    public int hp = 0;
    public int damage = 0;
    public float speed = 0;
    public float beforeDelay = 0;
    public float afterDelay = 0;
}
