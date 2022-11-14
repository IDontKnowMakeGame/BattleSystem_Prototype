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

    // Astar
    private int g;
    private int h;
    public Cube parent;

    public int gCost
    {
        get { return g; }
        set { g = value; }
    }

    public int hCost
    {
        get { return h; }
        set { h = value; }
    }

    public int fCost
    {
        get { return g + h; }
    }

    public UnitBase GetUnit()
    {
        return TheUnitOn;
    }
    
    public void SetUnit(UnitBase unit)
    {
        TheUnitOn = unit;
    }
}