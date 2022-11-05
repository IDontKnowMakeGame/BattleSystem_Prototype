using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor : MapObject
{
    public int idx = 0;
    public Cube[,] cubes = new Cube[5, 5];
}
