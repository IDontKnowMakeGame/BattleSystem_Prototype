using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private PlayerStat _stat = new PlayerStat();
    [FormerlySerializedAs("_behaviours")] [SerializeField]
    private List<BehaviourEnum> behaviours = new List<BehaviourEnum>();
    private readonly List<PlayerBehaviour> playerBehaviours = new List<PlayerBehaviour>();
    [SerializeField] private Position pos = new Position();
    [field:SerializeField]
    public bool IsRotate { get; set; }
    [field:SerializeField]
    public bool IsMoving { get; set; }
    [field:SerializeField]
    public bool IsDash { get; set; }
    [field:SerializeField]
    public bool IsAttack { get; set; }
    public Position Pos
    {
        get => pos;
        set => pos = value;
    }
    private void Init()
    {
        foreach (var behaviour in behaviours)
        {
            var type = System.Type.GetType("Player" + behaviour.ToString());
            object obj = System.Activator.CreateInstance(type);
            var behave = obj as PlayerBehaviour;
            behave.ThisBase = this;
            playerBehaviours.Add(behave);
        }
    }

    private void Awake()
    {
        Init();
        
        foreach (var behaviour in playerBehaviours)
        {
            behaviour.Awake();
        }
    }

    private void Update()
    {
        foreach (var behaviour in playerBehaviours)
        {
            behaviour.Update();
        }
    }
}
