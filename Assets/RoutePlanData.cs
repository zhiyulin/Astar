using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePlanData {

   // private Rectangle cellMap;

    //public Rectangle CellMap
    //{
    //    get { return cellMap; }
    //}

    // 结点地图
    private GameObject[,] cellMap;

    public GameObject[,] CellMap
    {
        get { return cellMap; }
    }

    // 关闭列表
    private IList<AStarNode> closedList = new List<AStarNode>();
    public IList<AStarNode> ClosedList
    {
        get { return closedList; }
    }

     // 开放列表
    private IList<AStarNode> openedList = new List<AStarNode>();
    public IList<AStarNode> OpenedList
    {
        get { return openedList; }
    }

    private Vector2 destination;
    // 终点
    public Vector2 Destination
    {
        get { return destination; }
    }

    public RoutePlanData(GameObject[,] map, Vector2 _destination)
    {
        this.cellMap = map;
        this.destination = _destination;
    }
}
