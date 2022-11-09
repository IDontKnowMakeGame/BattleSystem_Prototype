using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : MapObject
{
    public int Idx;
    public bool CanMoveOn = true;
    public bool IsPlayerOn = false;
    public UnitBase TheUnitOn = null;

    public UnitBase GetUnit()
    {
        return TheUnitOn;
    }
    
    public void SetUnit(UnitBase unit)
    {
        TheUnitOn = unit;
    }
}