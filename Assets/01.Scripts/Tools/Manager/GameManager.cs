using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManagerEnum
{
    Map,
}
public class GameManager : MonoBehaviour
{
    public GameObject floorObject; 
    public List<GameObject> diceObjects = new List<GameObject>();
    public List<ManagerEnum> Managers = new List<ManagerEnum>();
    private List<IManager> _managers = new List<IManager>();

    private void Init()
    {
        foreach (var manager in Managers)
        {
            var type = System.Type.GetType(manager.ToString() + "Manager");
            var instance = type.GetProperty("Instance");
            object obj = System.Activator.CreateInstance(type);
            instance.SetValue(type, obj, null);
            var _manager = obj as IManager;
            _manager.ParentManager = this;
            _managers.Add(_manager);
        }
    }

    private void Awake()
    {
        Init();

        foreach (var manager in _managers)
        {
            manager.Awake();
        }
    }

    private void Start()
    {
        foreach (var manager in _managers)
        {
            manager.Start();
        }
    }

    private void Update()
    {
        foreach (var manager in _managers)
        {
            manager.Update();
        }
    }

    private void LateUpdate()
    {
        foreach (var manager in _managers)
        {
            manager.LateUpdate();
        }
    }
}
