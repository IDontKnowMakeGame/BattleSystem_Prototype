using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : MapObject
{
    public int Idx;
    public bool CanMoveOn = true;
    public bool IsPlayerOn = false;
    public UnitBase TheUnitOn = null;
    private MeshRenderer meshRenderer = null;
    private Sequence seq;
    private Color originalColor;

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

    public void Attack(int damage)
    {
        if (thisObject == null) return;
        meshRenderer ??= thisObject.GetComponent<MeshRenderer>();

        seq = DOTween.Sequence();
        originalColor = meshRenderer.material.color;
        seq.Append(meshRenderer.material.DOColor(Color.red, 0.5f));
        seq.AppendCallback(() =>
            {
                if (Define.PlayerBase.Pos.GamePos == pos.GamePos)
                {
                    Define.PlayerBase.stat.TakeDamage(damage);
                }
            }
        );
        seq.Append(meshRenderer.material.DOColor(originalColor, 0.5f));
        seq.AppendCallback(() =>
        {
            seq.Kill();
        });
    }
}