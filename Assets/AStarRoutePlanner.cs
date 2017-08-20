using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public struct Point
//{
//    int x;
//    int y;
//};

public class AStarRoutePlanner {

    // 测试 停止次数
    private static int stopNum = 0;
    private int lineCount = 10;           // 行
    private int columnCount = 10;     // 列
    public GameObject[,] map;

    private ICostGetter costGetter = new SimpleCostGetter();
    private bool[][] obstacles = null;       // 不可通行位置

    public AStarRoutePlanner() : this(10, 10, new SimpleCostGetter()) { }
    public AStarRoutePlanner(int _lineCount, int _columnCount, ICostGetter _costGetter)
    {
        this.lineCount = _lineCount;
        this.columnCount = _columnCount;
        this.costGetter = _costGetter;
        this.InitObstacles();
    }

    // 初始化所有位置标记为无障碍
    private void InitObstacles()
    {
        this.obstacles = new bool[this.columnCount][];
        for (int i = 0; i < this.columnCount; i++)
        {
            this.obstacles[i] = new bool[this.lineCount];
        }
        for (int i = 0; i < this.columnCount; i++)
        {
            for (int j = 0; j < this.lineCount; j++)
            {
                this.obstacles[i][j] = false;
            }
        }
    }

    // 设置障碍位置
    public void Init(IList<Vector2> obstaclePoints,Vector2 startPos,Vector2 endPos)
    {
        if (obstacles != null)
        {
            foreach (Vector2 v in obstaclePoints)
            {
                Debug.Log(v.x+" "+v.y);
                this.obstacles[(int)v.x][(int)v.y] = true;
            }
        }
    }

    public IList<Vector2> Plan(Vector2 start, Vector2 destination)
    {
        map = new GameObject[this.columnCount, this.lineCount];

        //foreach (GameObject o in map)
        //{
        //    GameObject o  = GameObject.Instantiate(Resources.Load("Cube"),new Vector2())
        //}

        

        for (int i=0; i < this.columnCount; i++)
        {
            for (int j=0; j < this.lineCount ; j++)
            {

                //Debug.Log(".............................");
                GameObject o = GameObject.Instantiate(Resources.Load("Cube"), new Vector2(i, j), Quaternion.identity) as GameObject;
                map[i, j] = o;
                 if (this.obstacles[i][j])
                     map[i, j].GetComponent<MeshRenderer>().material.color = Color.blue;
               // Debug.Log(map[i, j]+" "+i+" "+j);
            }
        }

        Debug.Log(start.x + " " + start.y);

        

            //if ((!map.Contains(start)) || (!map.Contains(destination)))
            //{
            //   // throw new Exception("StartPoint or Destination not in the current map!");
            //    Debug.Log("StartPoint or Destination not in the current map!");
            //}

            if (start.x < 0 || start.x > columnCount || start.y < 0 || start.y > lineCount)
            {
                Debug.Log("StartPoint or Destination not in the current map!");
            }

        // 用来记录地图 开放列表 关闭列表
        RoutePlanData routePlanData = new RoutePlanData(map, destination);
        // 创建起始结点
        AStarNode startNode = new AStarNode(start, null, 0, 0);
        routePlanData.OpenedList.Add(startNode);

        AStarNode curNode = startNode;

        // 从起始节点开始进行递归调用
        return DoPlan(routePlanData, curNode);
    }

    public IList<Vector2> DoPlan(RoutePlanData routePlanData, AStarNode curNode)
    {
        stopNum++;

        IList<Dir> allDir = DirHelper.GetAllDir();

        // 遍历8个方向
        foreach (Dir dir in allDir)
        {
            Vector2 nextNode = GeometryHelper.GetAdjacentPoint(curNode.Location, dir);
            //if (!RoutePlanData.CellMap.Contains(nextNode))
            //    continue;
            if (nextNode.x < 0 || nextNode.x > routePlanData.CellMap.GetLength(0) || nextNode.y < 0 || nextNode.y > routePlanData.CellMap.GetLength(1))
                continue;
            if (this.obstacles[(int)nextNode.x][((int)nextNode.y)])
                continue;

            int costG = this.costGetter.GetCost(curNode.Location, dir);
            int costH = 10 * (Mathf.Abs((int)nextNode.x - (int)routePlanData.Destination.x) + Mathf.Abs((int)nextNode.y - (int)routePlanData.Destination.y));

            Debug.Log(curNode.Location.x + " " + curNode.Location.y + " " + costG);

            // costH为0找到终点
            if(costH == 0 || stopNum > 10)      
            {
                IList<Vector2> route = new List<Vector2>();
                route.Add(routePlanData.Destination);
                route.Insert(0,curNode.Location);
                AStarNode tempNode = curNode;
                while(tempNode.PreviousNode != null)
                {
                    route.Insert(0,tempNode.PreviousNode.Location);
                    tempNode = tempNode.PreviousNode;
                }
                return route;
            }
            
            AStarNode existNode = this.GetNodeOnLocation(nextNode,routePlanData);
            if(existNode != null){
                //if(existNode.CostG < costG+curNode.CostG){
                //    existNode.ResetPreviousNode(curNode,costG +  curNode.CostG);
                //}
                if (existNode.CostG > costG)
                {
                    existNode.ResetPreviousNode(curNode, costG);
                }
            }else if(!isExistInCloseList(nextNode,routePlanData)){
                AStarNode newNode = new AStarNode(nextNode,curNode,costG,costH);
                routePlanData.OpenedList.Add(newNode);
            }
        }

        // 将已遍历过的结点从开放列表转移到关闭列表
        routePlanData.OpenedList.Remove(curNode);
        routePlanData.ClosedList.Add(curNode);

        AStarNode minCostNode = this.GetMinCostNode(routePlanData.OpenedList);
        // 没有通路
        if (minCostNode == null)
        {
            return null;
        }

        
        //Debug.Log("absdsadd");
        // 对开放列表中的下一个代价最小的节点递归调用
        return this.DoPlan(routePlanData,minCostNode);
    }

    private AStarNode GetNodeOnLocation(Vector2 location, RoutePlanData routePlanData)
    {
        foreach (AStarNode temp in routePlanData.OpenedList)
        {
            if (temp.Location == location)
            {
                return temp;
            }
        }
        //foreach (AStarNode temp in routePlanData.ClosedList)
        //{
        //    if (temp.Location == location)
        //    {
        //        return temp;
        //    }
        //}
        return null;
    }

    // 结点是否在关闭列表中
    private bool isExistInCloseList(Vector2 location, RoutePlanData routePlanData)
    {
        foreach (AStarNode temp in routePlanData.ClosedList)
        {
            if (temp.Location == location)
            {
                return true;
            }
        }
        return false;
    }

    // 从开放列表获得代价F最小的节点
    private AStarNode GetMinCostNode(IList<AStarNode> openedList)
    {
        if (openedList.Count == 0)
        {
            return null;
        }

        AStarNode target = openedList[0];
        foreach (AStarNode temp in openedList)
        {
            if (temp.CostF < target.CostF)
            {
                target = temp;
            }
        }
        return target;
    }
}
