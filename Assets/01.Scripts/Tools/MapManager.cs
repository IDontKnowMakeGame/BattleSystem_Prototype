using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Floor[,] Map = new Floor[1000,1000];
    [SerializeField] private GameObject _floorObject; 
    [SerializeField] private List<GameObject> diceObjects = new List<GameObject>();
    [SerializeField] private float _distance = 1.5f;
    [SerializeField] private float _diceSize = 1f;
    [SerializeField] private int _mapDivide = 5;
    [SerializeField] private int _mapCount = 0;

    private void Awake()
    {
        var newData = new int[10, 10];
        _mapDivide = newData.GetLength(1) / 5;
        var col = newData.GetLength(0) / 5;
        for(int i = 0; i < col; i++)
            for(int j = 0; j < _mapDivide; j++)
                InitMap(newData, i * _mapDivide + (j % _mapDivide));
    }

    public void InitMap(int[ , ] mapData, int arr)
    {
        var rowLength = 5;
        var MainMap = new Cube[1000, 1000];
        var Floor = new Floor
        {
            pos = new Position
            {
                WorldPos = new Vector3((arr % _mapDivide) * 5 + 2, 0, (arr / _mapDivide) * 5 + 2) * _distance
            }
        };
        int x = 0, y = 0, cnt = 0;
        for(int i = 0; i < 5; i++)
        for (int j = 0; j < 5; j++)
        {
            x = (arr % _mapDivide) * 5 + (cnt % rowLength);
            y = (arr / _mapDivide) * 5 + (cnt / rowLength);
            var temp = MainMap[y, x] = new Cube();
            temp.thisObject = diceObjects[mapData[y, x]];
            Debug.Log($"{x}, {y}");
            temp.pos = new Position
            {
                WorldPos = new Vector3(x, 0, y) * _distance
            };
            cnt++;
        }
        GenerateMap(Floor, MainMap);
    }
    public void GenerateMap(Floor floor, Cube[,] MainMap)
    {
        var floorObject = Instantiate(_floorObject, transform);
        floorObject.transform.position = floor.pos.WorldPos;
        floorObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        floor.idx = _mapCount;
        floorObject.name = $"Floor:#{floor.idx}";
        floor.thisObject = floorObject;
        foreach (var cube in MainMap)
        {
            if (cube == null || !cube.thisObject)
                continue;
            var Cube = Instantiate(cube.thisObject, floorObject.transform);
            Cube.transform.localEulerAngles = Vector3.zero;
            Cube.transform.position = cube.pos.WorldPos;
            Cube.transform.localScale *= _diceSize;
            Cube.name = $"Cube:#{cube.pos.GamePos.z * 5 + cube.pos.GamePos.x}";
            cube.thisObject = Cube;
        }

        _mapCount++;
        floor.cubes = MainMap;
        int x = floor.idx % _mapDivide;
        int y = floor.idx / _mapDivide;
        Map[y, x] = floor;
    }
}
