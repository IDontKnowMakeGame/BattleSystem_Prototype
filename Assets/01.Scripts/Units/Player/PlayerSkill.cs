using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : IBehaviour
{
	public UnitBase ThisBase { get; set; }

	public UnitBase TargetBase { get; set; }

	protected bool isSkill;
	public SwordType thisSwordType
	{
		get { return _thisSwordType; }
		set
		{
			_thisSwordType = value;
			var type = System.Type.GetType("Player" + value.ToString() + "Skill");
			Debug.Log(type);
			var obj = System.Activator.CreateInstance(type);
			var behave = obj as ISkill;
			behave.ThisBase = ThisBase;
			behaviour = behave;
		}
	}

	private SwordType _thisSwordType;
	private ISkill behaviour;

	public virtual void Init()
	{
	}
	public virtual void Awake()
	{
		behaviour.ThisBase = ThisBase;
		behaviour.Awake();
	}


	public virtual void LateUpdate()
	{
		behaviour.LateUpdate();
	}

	public virtual void Start()
	{
		behaviour.Start();
	}

	public virtual void Update()
	{
		behaviour.Update();
	}
}
