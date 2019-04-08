using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Room
{
    private enum tileType { floor, wall, room };
    private enum RoomType { empty, bedroom, classroom };
    public int X1, Y1, X2, Y2;

    public int roomType = (int)RoomType.empty;

    public int[,] roomData;

    public Room(int x1, int y1, int x2, int y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;

        roomData = new int[X2 - X1, Y2 - Y1];
        CreateRoomData(roomType);
    }

    public void CreateRoomData(int roomType)
    {
        switch (roomType)
        {
            case (int)RoomType.empty:
                for (int i = 0; i < X2 - X1; i++)
                {
                    for (int j = 0; j < Y2 - Y1; j++)
                    {
                        if (i == 0 || i == X2 - X1 - 1 || j == 0 || j == Y2 - Y1 - 1)
                        {
                            roomData[i, j] = (int)tileType.wall;
                        }
                        else
                        {
                            roomData[i, j] = (int)tileType.room;
                        }
                        //roomData[i, j] = (int)tileType.room;
                    }
                }
                break;
            default:
                for (int i = 0; i < X2 - X1; i++)
                {
                    for (int j = 0; j < Y2 - Y1; j++)
                    {
                        if (i == 0 || i == X2 - X1 - 1 || j == 0 || j == Y2 - Y1 - 1)
                        {
                            roomData[i, j] = (int)tileType.wall;
                        }
                        else
                        {
                            roomData[i, j] = (int)tileType.room;
                        }
                        //roomData[i, j] = (int)tileType.room;
                    }
                }
                break;
        }

    }

    public bool outOfMap(int[,] map)
    {
        return false;
    }

    /// <summary>
    /// 判断房间是否和其他房间有重叠
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool inAnotherRoom(Room other)
    {
        if (X1 >= other.X1 && X1 <= other.X2 && Y1 >= other.Y1 && Y1 <= other.Y2)
        {
            return true;
        }
        if (X2 <= other.X2 && X2 >= other.X1 && Y2 <= other.Y2 && Y2 >= other.Y1)
        {
            return true;
        }
        if (X1 >= other.X1 && X1 <= other.X2 && Y2 >= other.Y1 && Y2 <= other.Y2)
        {
            return true;
        }
        if (X2 <= other.X2 && X2 >= other.X1 && Y1 <= other.Y2 && Y1 >= other.Y1)
        {
            return true;
        }

        return false;
    }
}