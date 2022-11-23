using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGreatSwordSkill : ISkill
{
	public UnitBase ThisBase { get; set; }
	public UnitBase TargetBase { get; set; }

	private bool _isSkill
	{
		get
		{
			return ((PlayerBase)ThisBase).isSkill;
		}
		set
		{
			((PlayerBase)ThisBase).isSkill = value;
		}
	}
	private bool _isCoolTime;
	private bool _istimer = false;

	public float maxCoolTime = 10f;//=> _stat.weapon.skillStat.CoolTime;//이건 공통
	public float commendTime = 0.2f;
	public float noDamageTime = 1f; //=> ((GreatSwordSO)_stat.weapon.skillStat).noDamageTime;
	public float maxStunTime = 3f; //=> ((GreatSwordSO)_stat.weapon.skillStat).maxStunTime;

	private float _waitForCommand = 0;
	private float coolTime = 0f;

	private PlayerBase _thisPlayerBase;
	private PlayerStat _stat;
	private WeaponStat _weaponStat;


	public virtual void Init()
	{

	}

	public virtual void Awake()
	{
		_thisPlayerBase = ThisBase as PlayerBase;
		_stat = _thisPlayerBase.stat;
		_weaponStat = _stat.weapon._weaponStat;
	}

	public virtual void Start()
	{
		_isSkill = false;

		_waitForCommand = 0;
	}

	public virtual void Update()
	{
		InputTimer();

		if (Input.GetKeyDown(KeyCode.Space) && !_isSkill && !_isCoolTime)
		{
			if (!_isSkill)
			{
				ThisBase.StartCoroutine(GodTime());
			}
		}

		DoDashAttack();
	}

	private void InputTimer()
	{
		if (_istimer)
			_waitForCommand += Time.deltaTime;
		if (_waitForCommand >= noDamageTime && _istimer)
		{
			_istimer = false;
			_waitForCommand = 0;
		}

		if (_isCoolTime)
		{
			if (maxCoolTime > coolTime)
				coolTime += Time.deltaTime;
			else
			{
				_isCoolTime = false;
				coolTime = 0;
			}
		}
	}

	private void DoDashAttack()
	{
		if (_isSkill && _istimer)
		{
			if (Input.GetKeyDown(KeyCode.W))
			{
				ThisBase.StartCoroutine(DoHavyAttack(Vector3Int.forward));
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				ThisBase.StartCoroutine(DoHavyAttack(Vector3Int.left));
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				ThisBase.StartCoroutine(DoHavyAttack(Vector3Int.right));
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				ThisBase.StartCoroutine(DoHavyAttack(Vector3Int.back));
			}

			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				_stat.canAttackable = true;
				_isSkill = false;
				_isCoolTime = true;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				_stat.canAttackable = true;
				_isSkill = false;
				_isCoolTime = true;
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				_stat.canAttackable = true;
				_isSkill = false;
				_isCoolTime = true;
			}
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				_stat.canAttackable = true;
				_isSkill = false;
				_isCoolTime = true;
			}
		}
	}

	private IEnumerator DoHavyAttack(Vector3Int vec)
	{
		((PlayerMove)_thisPlayerBase.BehaviourGet<PlayerMove>()).MovePlayer(vec);

		yield return new WaitForSeconds(_thisPlayerBase.stat.GetStat().speed);

		_stat.canAttackable = true;
		var atkPos = ThisBase.Pos.GamePos + vec;
		var atkCube = MapManager.GetCube(atkPos);
		TargetBase = atkCube?.TheUnitOn;

		if (TargetBase is null)
			_thisPlayerBase.stat.ReduceAdrenaline(5);
		else
		{
			((EnemyBase)TargetBase).stat.TakeDamage(_thisPlayerBase.stat.GetStat().damage, 1);
			ThisBase.StartCoroutine(StunTime());
			//GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f;
			_thisPlayerBase.stat.AddAdrenaline(10);
		}

		ThisBase.isAttack = true;
		_isCoolTime = true;
		_isSkill = false;
	}

	private IEnumerator GodTime()
	{
		_istimer = true;
		_isSkill = true;
		_stat.canAttackable = false;

		yield return new WaitForSeconds(commendTime);

		_stat.canAttackable = true;
		_isSkill = false;
		_isCoolTime = true;
	}

	public IEnumerator StunTime()
	{
		EnemyBase enemy = ((EnemyBase)TargetBase);
		enemy.IsStun = true;
		yield return new WaitForSeconds(maxStunTime);
		enemy.IsStun = false;
	}
	public virtual void LateUpdate()
	{

	}
}
