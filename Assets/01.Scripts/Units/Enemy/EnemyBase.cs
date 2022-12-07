using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    public EnemyStat stat = new EnemyStat();

	protected override void Init()
	{
		stat.basicHPSlider.InitSlider(stat.hp);
	}	
}
