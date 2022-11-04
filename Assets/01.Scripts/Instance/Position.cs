using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Position
{
    [SerializeField] private Vector3 worldPos;
    public Vector3 WorldPos
    {
        get => worldPos;
        set
        {
            gamePos = new Vector3Int(Mathf.RoundToInt(value.x / 1.5f), 0, Mathf.RoundToInt(value.z / 1.5f));
            worldPos = value;
        }
    }
    [SerializeField] private Vector3Int gamePos;
    public Vector3Int GamePos => gamePos;
}
