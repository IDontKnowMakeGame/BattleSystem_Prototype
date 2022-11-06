using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    public GameManager ParentManager { get; set; }
    public void Awake(); 
    public void Start();
    public void Update();
    public void LateUpdate();
}
