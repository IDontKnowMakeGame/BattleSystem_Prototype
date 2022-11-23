using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : UnitBase
{
    public PlayerStat stat = new PlayerStat();
	public bool isSkill { get; set; }
	protected override void Init()
    {
        foreach (var behaviour in behaviours)
        {
            var type = System.Type.GetType("Player"+ behaviour.ToString());
            if (type == null) continue;
            var obj = System.Activator.CreateInstance(type);
            var behave = obj as IBehaviour;
            if (behave == null) continue;
            behave.ThisBase = this;
			if (behave is PlayerSkill)
			{
				for (int i = 0; i < (int)SwordType.END; i++)
				{
					if (((SwordType)i).ToString() == stat.weapon.name)
					{
						PlayerSkill skill = (PlayerSkill)behave;
						skill.thisSwordType = (SwordType)i;
					}
				}
			}

            _behaviours.Add(behave);
		}
    }
	public IBehaviour BehaviourGet<T>()
	{
		return _behaviours.Find(x => x is T);
	}
	protected override void Awake()
    {
        base.Awake();
        stat.ThisPlayer = this;
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
            stat.TakeDamage(10, 0);
        stat.Update();
    }
}
