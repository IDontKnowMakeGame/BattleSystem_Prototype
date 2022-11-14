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


    // To Do 불려오는 방식 바꿔보기
    private Astar pathFinding;
    public Astar PathFinding
    {
        get
        {
            if(pathFinding == null)
            {
                GameObject.Find("Astar").GetComponent<Astar>();
            }
            return pathFinding;
        }
    }

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
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            //Debug.Log(GetCube(5, 3));
        }
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

    public List<Cube> GetNeighbours(Cube node)
    {
        List<Cube> neighbours = new List<Cube>();
        // 상하좌우 세팅
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        bool[] walkableUDLR = new bool[4];

        // 상하좌우의 노드 계산
        for(int i = 0; i < 4; i++)
        {
            int checkX = node.pos.GamePos.x + temp[i, 0];
            int checkY = node.pos.GamePos.z + temp[i, 1];

            // To do 맵 크기 5로 가정
            if (checkX >= 0 && checkX < 5 && checkY >= 0 && checkY < 5)
            {
                if(GetCube(checkX, checkX).CanMoveOn)
                {
                    walkableUDLR[i] = true;
                    neighbours.Add(GetCube(checkX, checkY));
                }
            }
        }

        // To Do 필요하면 대각선의 노드도 계산하기

        return neighbours;
    }

    public void MoveUnitOn(UnitBase unitBase)
    {
        Vector3Int curPos = unitBase.Pos.GamePos;
        var cube = GetCube(curPos.x, curPos.z);
        cube.TheUnitOn = unitBase;
        Debug.Log($"{unitBase.name} is on {curPos}");
        MoveUnitOn(unitBase.Pos.GamePos, unitBase);
    }
    public void MoveUnitOn(Vector3Int cur, Vector3Int next) => MoveUnitOn(cur.x, cur.z, next.x, next.z);
    public void MoveUnitOn(Vector3Int cur, Vector3Int next, UnitBase unitBase) => MoveUnitOn(cur.x, cur.z, next.x, next.z, unitBase);
    public void MoveUnitOn(Vector3Int cur, UnitBase unitBase) => MoveUnitOn(cur.x, cur.z, cur.x, cur.z, unitBase);
    public void MoveUnitOn(Vector3Int cur) => MoveUnitOn(cur.x, cur.z, cur.x, cur.z);
    public void MoveUnitOn(int ox, int oy, int nx, int ny, UnitBase unit = null)
    {
        var cube = GetCube(ox, oy);
        cube.IsPlayerOn = false;
        cube.SetUnit(null);

        cube = GetCube(nx, ny);
        cube.IsPlayerOn = true;
        cube.SetUnit(unit);
    }

    public void MoveUnitOn(int ox, int oy, int nx, int ny)
    {
       MoveUnitOn(ox, oy, nx, ny, null);
    }
    
    public static Cube GetCube(Vector3Int pos) => GetCube(pos.x, pos.z);
    public static Cube GetCube(int x, int y)
    {
        var floorX = x / 5;
        var floorY = y / 5;
        if(x < 0 || x >= Instance.mapCount * 5 / 2 || y < 0 || y >= Instance.mapCount * 5 /2)
            return null;
        var floor = Instance.map[floorY, floorX];
        var cube = floor?.cubes[(y % 5), (x % 5)]; 
        return cube;
    }
}
