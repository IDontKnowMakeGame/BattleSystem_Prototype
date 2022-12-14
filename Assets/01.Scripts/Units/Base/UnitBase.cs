using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] protected List<BehaviourEnum> behaviours = new List<BehaviourEnum>();
    protected readonly List<IBehaviour> _behaviours = new List<IBehaviour>();
    [SerializeField] private Position pos = new Position();
    [field:SerializeField] 
    public bool IsRotate { get; set; }
    [field:SerializeField] 
    public bool IsForward { get; set; }
    [field:SerializeField]
    public bool IsMoving { get; set; }
    [field:SerializeField]
    public bool IsDash { get; set; }
    [field:SerializeField]
    public bool IsAttack { get; set; }
    public bool isAttack { get; set; }
	[field: SerializeField]
	public bool IsStun { get; set; }
	public Position Pos
    {
        get => pos;
        set => pos = value;
    }
    protected virtual void Init()
    {
        foreach (var behaviour in behaviours)
        {
            var type = System.Type.GetType(behaviour.ToString());
            object obj = System.Activator.CreateInstance(type);
            var behave = obj as IBehaviour;
            Debug.Log(behave);
            behave.ThisBase = this;
            _behaviours.Add(behave);
        }
    }
    
    protected virtual void Awake()
    {
        Init();
        pos.WorldPos = transform.localPosition;
        foreach (var behaviour in _behaviours)
        {
			if (IsStun)
				return;
			behaviour.Awake();
        }
    }
    
    
    protected virtual void Start()
    {
        MapManager.Instance.MoveUnitOn(this);
        foreach (var behaviour in _behaviours)
        {
			if (IsStun)
				return;
			behaviour.Start();
        }
    }
    
    protected virtual void Update()
    {
        foreach (var behaviour in _behaviours)
        {
			if (IsStun)
				return;
			behaviour.Update();
        }
    }
    
    protected virtual void LateUpdate()
    {
        foreach (var behaviour in _behaviours)
        {
			if (IsStun)
				return;
			behaviour.LateUpdate();
        }
    }
}
