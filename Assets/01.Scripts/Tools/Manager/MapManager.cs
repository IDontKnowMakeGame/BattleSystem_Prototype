using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.WSA;
using Object = UnityEngine.Object;

public class MapManager : IManager
{
    private readonly Floor[,] map = new Floor[1000,1000];
    private float distance = 1.5f;
    private float diceSize = 1f;
    private int mapDivide = 5;
    private int mapCount = 0;
    
    public static MapManager Instance { get; set; }

    public GameManager ParentManager { get; set; }

    public void Awake()
    {
        var newData = new[,] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };
        mapDivide = newData.GetLength(1) / 5;
        var col = newData.GetLength(0) / 5;
        for(var i = 0; i < col; i++)
            for(var j = 0; j < mapDivide; j++)
                InitMap(newData, i * mapDivide + (j % mapDivide));
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void LateUpdate()
    {
        
    }

    private void InitMap(int[ , ] mapData, int arr)
    {
        const int rowLength = 5;
        var mainMap = new Cube[5, 5];
        var floor = new Floor
        {
            pos = new Position
            {
                WorldPos = new Vector3((arr % mapDivide) * 5 + 2, 0, (arr / mapDivide) * 5 + 2) * distance
            }
        };
        var cnt = 0;
        for(var i = 0; i < 5; i++)
        for (var j = 0; j < 5; j++)
        {
            var x = (arr % mapDivide) * 5 + (cnt % rowLength);
            var y = (arr / mapDivide) * 5 + (cnt / rowLength);
            var temp = mainMap[i, j] = new Cube();
            temp.thisObject = ParentManager.diceObjects[mapData[y, x]];
            Debug.Log($"{x}, {y}");
            temp.Idx = i * 5 + j;
            temp.pos = new Position
            {
                WorldPos = new Vector3(x, 0, y) * distance
            };
            cnt++;
        }
        GenerateMap(floor, mainMap);
    }

    private void GenerateMap(Floor floor, Cube[,] mainMap)
    {
        var floorThisObject = Object.Instantiate(ParentManager.floorObject, ParentManager.transform.GetChild(0));
        floorThisObject.transform.position = floor.pos.WorldPos;
        floorThisObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        floor.idx = mapCount;
        floorThisObject.name = $"Floor:#{floor.idx}";
        floor.thisObject = floorThisObject;
        foreach (var cube in mainMap)
        {
            if (cube == null || !cube.thisObject)
            {
                cube.CanMoveOn = false;
                continue;
            }
            var cubeObject = Object.Instantiate(cube.thisObject, floorThisObject.transform);
            cubeObject.transform.localEulerAngles = Vector3.zero;
            cubeObject.transform.position = cube.pos.WorldPos;
            cubeObject.transform.localScale *= diceSize;
            cubeObject.name = $"Cube:#{cube.Idx}";
            cube.thisObject = cubeObject;
        }

        mapCount++;
        floor.cubes = mainMap;
        var x = floor.idx % mapDivide;
        var y = floor.idx / mapDivide;
        map[y, x] = floor;
    }

    public static bool NullCheckMap(int x, int y)
    {
        if (x < 0 || x >= Instance.mapCount * 5 / 2)
        {
            return false;
        }

        if (y < 0 || y >= Instance.mapCount * 5 / 2)
        {
            return false;
        }
        var floorX = x / 5;
        var floorY = y / 5;
        var floor = Instance.map[floorY, floorX];
        var cube = floor.cubes[(y % 5), (x % 5)];
        var canMove = cube.CanMoveOn && !cube.IsPlayerOn;

        return canMove;
    }

    public void MoveUnitOn(Vector3Int cur, Vector3Int next) => MoveUnitOn(cur.x, cur.z, next.x, next.z);
    public void MoveUnitOn(Vector3Int cur) => MoveUnitOn(cur.x, cur.z, cur.x, cur.z);

    public void MoveUnitOn(int ox, int oy, int nx, int ny)
    {
        var floorX = ox / 5;
        var floorY = oy / 5;
        var floor = Instance.map[floorY, floorX];
        var cube = floor.cubes[(oy % 5), (ox % 5)];
        cube.IsPlayerOn = false;
        
        floorX = nx / 5;
        floorY = ny / 5;
        floor = Instance.map[floorY, floorX];
        cube = floor.cubes[(ny % 5), (nx % 5)];
        cube.IsPlayerOn = true;
    }
    
}
