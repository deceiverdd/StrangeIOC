﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DARandomRoom : MonoBehaviour
{
    #region Parameter

    //地图大小
    public int DungeonHeight = 128;
    public int DungeonWidth = 128;

    //房间大小
    public int roomMixSize = 2;
    public int roomMaxSize = 5;

    //预制引用
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject roomPrefab;
    public GameObject opendoorPrefab;
    public GameObject closedoorPrefab;

    public Sprite[] numbers;

    /// <summary>
    /// 生成房间次数
    /// </summary>
    public int createTimes = 100;

    /// <summary>
    /// 当前区域数量
    /// </summary>
    private int currentRegionNum = 0;

    /// <summary>
    /// 记录区域数组
    /// </summary>
    private int[,] regions;

    /// <summary>
    /// 记录房间数组
    /// </summary>
    private List<DARoom> rooms;

    /// <summary>
    /// 记录边缘数组
    /// </summary>
    private List<Vector2> bounds;

    /// <summary>
    /// 地图数据
    /// </summary>
    private int[,] mapArray;

    /// <summary>
    /// tile类型
    /// </summary>
    private enum tileType { floor, wall, room, opendoor, closedoor };

    /// <summary>
    /// 4个基本方向
    /// </summary>
    private Vector2[] baseDirection = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };

    /// <summary>
    /// 迷宫父物体
    /// </summary>
    private GameObject mazeParent;

    #endregion

    void Start()
    {
        mapArray = new int[DungeonHeight, DungeonWidth];
        mazeParent = new GameObject();
        mazeParent.name = "Maze";
        rooms = new List<DARoom>();
        bounds = new List<Vector2>();
        regions = new int[DungeonHeight, DungeonWidth];

        Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReCreateMap();
        }
    }

    /// <summary>
    /// 生成迷宫
    /// </summary>
    private void Generate()
    {
        CreatBoard(mapArray);

        for (int i = 0; i < createTimes; i++)
        {
            AddRooms();
        }

        GrowMaze(new Vector2(1, 1));

        ConnectRegions();

        //RemoveDeadEnds();

        DrawMaze();
    }

    /// <summary>
    /// 增加房间
    /// </summary>
    private void AddRooms()
    {
        DARoom room = CreateRoom();

        bool overLap = false;

        if (room.X2 >= DungeonWidth || room.Y2 >= DungeonWidth)
        {
            overLap = true;
        }

        if (room.X1 >= DungeonWidth || room.Y1 >= DungeonWidth)
        {
            overLap = true;
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            if (room.inAnotherRoom(rooms[i]))
            {
                overLap = true;
                break;
            }
        }

        if (!overLap)
        {
            rooms.Add(room);
            currentRegionNum++;
            FillRoom(room);
        }
    }

    /// <summary>
    /// 初始化迷宫
    /// </summary>
    /// <param name="map"></param>
    private void CreatBoard(int[,] map)
    {
        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                map[i, j] = (int)tileType.wall;

                if (i == -1 || j == -1 || i == DungeonWidth || j == DungeonHeight)
                {
                    bounds.Add(new Vector2(i, j));
                }
            }
        }
    }

    /// <summary>
    /// 生成过道
    /// </summary>
    /// <param name="start"></param>
    private void GrowMaze(Vector2 start)
    {
        List<Vector2> cells = new List<Vector2>();
        List<Vector2> unmadeCells = new List<Vector2>();
        Vector2 cell;

        _startRegion();
        _carve(start, tileType.floor);
        cells.Add(start);

        while (cells.Count > 0)
        {
            cell = cells[cells.Count - 1];
            unmadeCells = new List<Vector2>();
            foreach (Vector2 dir in baseDirection)
            {
                if (_canCarve(cell, dir))
                    unmadeCells.Add(dir);
            }

            if (unmadeCells.Count > 0)
            {
                Vector2 dir;

                //Debug.Log(unmadeCells[0]);
                dir = unmadeCells[Random.Range(0, unmadeCells.Count)];
                _carve(cell + dir, tileType.floor);
                _carve(cell + dir * 2, tileType.floor);
                cells.Add(cell + dir * 2);
            }
            else
            {
                cells.RemoveAt(cells.Count - 1);
            }
        }
    }

    private bool _canCarve(Vector2 pos, Vector2 direction)
    {
        // Must end in bounds.
        if (bounds.Contains(pos + direction * 3))
            return false;

        // Destination must not be open.
        Vector2 temppos = pos + direction * 2;

        if (temppos.x > 0 && temppos.x < DungeonWidth - 1 && temppos.y > 0 & temppos.y < DungeonHeight - 1)
        {
            return mapArray[(int)temppos.x, (int)temppos.y] == (int)tileType.wall;
        }
        else
        {
            return false;
        }
    }

    private void _carve(Vector2 pos, tileType type)
    {
        mapArray[(int)pos.x, (int)pos.y] = (int)type;
        regions[(int)pos.x, (int)pos.y] = currentRegionNum;
    }

    private void _startRegion()
    {
        currentRegionNum++;
    }

    /// <summary>
    /// 连接区域
    /// </summary>
    private void ConnectRegions()
    {
        //找出连接多个区域的连接点
        List<Vector2>[] connectors = new List<Vector2>[currentRegionNum];
        Debuger.Log(currentRegionNum);

        for (int k = 1; k < currentRegionNum; k++)
        {
            connectors[k] = new List<Vector2>();
            for (int i = 1; i < DungeonWidth - 1; i++)
            {
                for (int j = 1; j < DungeonHeight - 1; j++)
                {
                    if (RoundWallCount(i, j) > 2)
                        continue;

                    if (mapArray[i, j] != (int)tileType.wall)
                        continue;

                    if (regions[i, j] == k)
                    {
                        if (regions[i + 1, j] != regions[i, j] && mapArray[i + 1, j] != (int)tileType.wall)
                        {
                            connectors[k].Add(new Vector2(i, j));
                            continue;
                        }

                        if (regions[i - 1, j] != regions[i, j] && mapArray[i - 1, j] != (int)tileType.wall)
                        {
                            connectors[k].Add(new Vector2(i, j));
                            continue;
                        }

                        if (regions[i, j + 1] != regions[i, j] && mapArray[i, j + 1] != (int)tileType.wall)
                        {
                            connectors[k].Add(new Vector2(i, j));
                            continue;
                        }

                        if (regions[i, j - 1] != regions[i, j] && mapArray[i, j - 1] != (int)tileType.wall)
                        {
                            connectors[k].Add(new Vector2(i, j));
                            continue;
                        }
                    }
                }
            }

            Vector2 temppos = new Vector2();

            if (connectors[k].Count > 0)
            {
                temppos = connectors[k][Random.Range(0, connectors[k].Count - 1)];
                mapArray[(int)temppos.x, (int)temppos.y] = (int)tileType.closedoor;
            }
        }
    }

    /// <summary>
    /// 删除死胡同
    /// </summary>
    private void RemoveDeadEnds()
    {
        //查找所有路的集合
        List<Vector2> floors = new List<Vector2>();

        for (int i = 1; i < DungeonWidth - 1; i++)
        {
            for (int j = 1; j < DungeonHeight - 1; j++)
            {
                if (mapArray[i, j] == (int)tileType.floor)
                {
                    floors.Add(new Vector2(i, j));
                }
            }
        }

        while (true)
        {
            bool noDeadLoad = true;

            foreach (Vector2 floor in floors)
            {
                if (RoundWallCount((int)floor.x, (int)floor.y) > 2)
                {
                    floors.Remove(floor);
                    mapArray[(int)floor.x, (int)floor.y] = (int)tileType.wall;
                    noDeadLoad = false;
                    break;
                }
            }

            if (noDeadLoad)
                break;
        }
    }

    /// <summary>
    /// 计算周边墙的数量
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <returns></returns>
    private int RoundWallCount(int i, int j)
    {
        int roundwallcount = 0;

        if (mapArray[i + 1, j] == (int)tileType.wall)
            roundwallcount++;

        if (mapArray[i - 1, j] == (int)tileType.wall)
            roundwallcount++;

        if (mapArray[i, j + 1] == (int)tileType.wall)
            roundwallcount++;

        if (mapArray[i, j - 1] == (int)tileType.wall)
            roundwallcount++;

        return roundwallcount;
    }

    /// <summary>
    /// 增加连接点
    /// </summary>
    private void AddJunction()
    {

    }

    /// <summary>
    /// 查找连接多区域的连接点
    /// </summary>
    private void FindConnector()
    {

    }

    /// <summary>
    /// 填充房间
    /// </summary>
    /// <param name="room"></param>
    private void FillRoom(DARoom room)
    {
        for (int i = room.X1; i < room.X2; i++)
        {
            for (int j = room.Y1; j < room.Y2; j++)
            {
                mapArray[i, j] = room.roomData[i - room.X1, j - room.Y1];
                regions[i, j] = currentRegionNum;
                bounds.Add(new Vector2(i, j));
            }
        }
    }

    /// <summary>
    /// 创建随机大小房间
    /// </summary>
    /// <returns></returns>
    private DARoom CreateRoom()
    {
        int roomHeight = Random.Range(roomMixSize, roomMaxSize) + 2;
        int roomWidth = Random.Range(roomMixSize, roomMaxSize) + 2;
        int x = Random.Range(1, DungeonWidth / 2) * 2;
        int y = Random.Range(1, DungeonHeight / 2) * 2;
        DARoom room = new DARoom(x, y, x + roomWidth, y + roomHeight);

        return room;
    }

    /// <summary>
    /// 绘制迷宫
    /// </summary>
    private void DrawMaze()
    {
        for (int i = 0; i < DungeonWidth; i++)
        {
            //string log = "";
            for (int j = 0; j < DungeonHeight; j++)
            {
                //log += mapArray[i, j];
                if (mapArray[i, j] == (int)tileType.floor)
                {
                    GameObject go = Instantiate(floorPrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mazeParent.transform);
                    go.transform.Find("number").GetComponent<SpriteRenderer>().sprite = numbers[regions[i,j]];
                }
                else if (mapArray[i, j] == (int)tileType.wall)
                {
                    GameObject go = Instantiate(wallPrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mazeParent.transform);
                    go.transform.Find("number").GetComponent<SpriteRenderer>().sprite = numbers[regions[i, j]];
                }
                else if (mapArray[i, j] == (int)tileType.room)
                {
                    GameObject go = Instantiate(roomPrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mazeParent.transform);
                    go.transform.Find("number").GetComponent<SpriteRenderer>().sprite = numbers[regions[i, j]];
                }
                else if (mapArray[i, j] == (int)tileType.opendoor)
                {
                    GameObject go = Instantiate(opendoorPrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mazeParent.transform);
                    go.transform.Find("number").GetComponent<SpriteRenderer>().sprite = numbers[regions[i, j]];
                }
                else if (mapArray[i, j] == (int)tileType.closedoor)
                {
                    GameObject go = Instantiate(closedoorPrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mazeParent.transform);
                    go.transform.Find("number").GetComponent<SpriteRenderer>().sprite = numbers[regions[i, j]];
                }
            }
        }
    }

    private void ReCreateMap()
    {
        mapArray = new int[DungeonHeight, DungeonWidth];
        bounds = new List<Vector2>();
        regions = new int[DungeonHeight, DungeonWidth];
        GameObject.Destroy(mazeParent);
        Destroy(GameObject.Find("Maze"));
        mazeParent = new GameObject();
        mazeParent.name = "Maze";
        rooms = new List<DARoom>();
        Generate();
    }

    /// <summary>
    /// 计算周围墙的数量 参数t为计算几圈
    /// </summary>
    /// <param name="array"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private int CheckNeighborWalls(int[,] array, int i, int j, int t)
    {
        int count = 0;

        for (int i2 = i - t; i2 < i + t + 1; i2++)
        {
            for (int j2 = j - t; j2 < j + t + 1; j2++)
            {
                if (i2 > 0 && i2 < DungeonWidth && j2 >= 0 && j2 < DungeonHeight)
                {
                    if (array[i2, j2] == (int)tileType.wall)
                    {
                        count++;
                    }
                }
            }
        }

        if (array[i, j] == (int)tileType.wall)
            count--;//因为上面的循环包含了自身，属于要将自身减掉

        return count;
    }
}





