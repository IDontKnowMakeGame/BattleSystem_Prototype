using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public static class EnemyAttack
{
    public static void HorizontalAttack(EnemyBase thisBase, int z, int range, int damage)
    {
        var pos = thisBase.Pos;
        for(int r = -range; r <= range; r++)
        {
            MapManager.Boom(pos.GamePos.AddX(r).SetZ(z), damage);
        }
    }
    
    public static void VerticalAttack(EnemyBase thisBase, int x, int range, int damage)
    {
        var pos = thisBase.Pos;
        for(int r = -range; r <= range; r++)
        {
            MapManager.Boom(pos.GamePos.SetX(x).AddZ(r), damage);
        }
    }

    public static void AddAttack(EnemyBase thisBase, Vector3Int dir, int damage)
    {
        var pos = thisBase.Pos;
        MapManager.Boom(pos.GamePos.AddVec(dir), damage);
    }

    public static void FourWayAttack(EnemyBase thisBase, int range, int damage)
    {
        AddAttack(thisBase, Vector3Int.forward * range, damage);
        AddAttack(thisBase, Vector3Int.back * range, damage);
        AddAttack(thisBase, Vector3Int.left * range, damage);
        AddAttack(thisBase, Vector3Int.right * range, damage);
    }

    public static void RangeAttack(EnemyBase thisBase, int range, int damage)
    {
            var pos = thisBase.Pos;
        for(int x = -range; x <= range; x++)
        {
            for(int z = -range; z <= range; z++)
            {
                MapManager.Boom(pos.GamePos.AddX(x).AddZ(z), damage);
            }
        }
    }
}
