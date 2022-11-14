using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Astar : MonoBehaviour
{
    public Cube start, end;
    public LineRenderer line;
    public void FindPath(Cube _start, Cube _end)
    {
        start = _start;
        end = _end;
        bool pathSuccess = false;

        List<Cube> openList = new List<Cube>(); // OPEN
        HashSet<Cube> closedList = new HashSet<Cube>(); // CLOSE
        openList.Add(start);

        while(openList.Count > 0)
        {
            Cube currentTile = openList[0];

            // Open List에서 FCost가 가장 작은 Tile 찾기
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost < currentTile.fCost || 
                    (openList[i].fCost == currentTile.fCost &&      
                    openList[i].hCost < currentTile.hCost))
                {
                    currentTile = openList[i];
                }
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            // 도착지점에 오면 종료
            if (currentTile == end)
            {
                pathSuccess = true;
                break;
            }

            // 이웃 노드를 검색
            foreach (Cube neighbour in MapManager.Instance.GetNeighbours(currentTile))
            {
                // 이동불가 노드이거나 이미 검색한 노드 제외
                if (!neighbour.CanMoveOn || closedList.Contains(neighbour))
                    continue;

                int nowCost = currentTile.gCost + GetDistance(currentTile, neighbour);
                if(nowCost < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = nowCost;
                    neighbour.hCost = GetDistance(neighbour, end);
                    neighbour.parent = currentTile;

                    if(!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }
        if(pathSuccess)
        {
            DrawingLine(RetracePath());
        }
    }
    
    // Tile 간의 거리 계산
    int GetDistance(Cube tileA, Cube tileB)
    {
        int destX = Mathf.Abs(tileA.pos.GamePos.x - tileB.pos.GamePos.x);
        int destY = Mathf.Abs(tileA.pos.GamePos.z - tileB.pos.GamePos.z);

        if (destX > destY)
            return 14 * destY + 10 * (destX - destY);
        return 14 * destX + 10 * (destY - destX);
    }

    Vector3[] RetracePath()
    {
        List<Cube> path = new List<Cube>();
        Cube currentTile = end;
        while(currentTile != start)
        {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        Vector3[] wayPoints = SimplefyPath(path);
        Array.Reverse(wayPoints);
        return wayPoints;
    }

    Vector3[] SimplefyPath(List<Cube> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 oldDir = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDir = new Vector2(path[i - 1].pos.GamePos.x - path[i].pos.GamePos.x, path[i - 1].pos.GamePos.z - path[i].pos.GamePos.z);
            if (oldDir != newDir)
            {
                waypoints.Add(path[i - 1].thisObject.transform.position + Vector3.back * 0.1f);
            }
            oldDir = newDir;
        }
        waypoints.Add(start.thisObject.transform.position + Vector3.back * 0.1f);
        return waypoints.ToArray();
    }

    public void DrawingLine(Vector3[] waypoints)
    {
        line.positionCount = waypoints.Length;
        for (int i = 0; i < waypoints.Length; i++)
        {
            line.SetPosition(i, waypoints[i]);
        }
    }
}
