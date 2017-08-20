using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour {

	// Use this for initialization
	void Start () {


        AStarRoutePlanner aStarRoutePlanner = new AStarRoutePlanner();
        IList<Vector2> obstaclePoints = new List<Vector2>();
        obstaclePoints.Add(new Vector2(2, 4));
        obstaclePoints.Add(new Vector2(3, 4));
        obstaclePoints.Add(new Vector2(4, 4));
        obstaclePoints.Add(new Vector2(5, 4));
        obstaclePoints.Add(new Vector2(6, 4));
        

        // 起点 
        Vector2 startPos = new Vector2(4, 3);
        // 终点
        Vector2 endPos = new Vector2(6, 7);

        aStarRoutePlanner.Init(obstaclePoints, startPos, endPos);

        IList<Vector2> route = aStarRoutePlanner.Plan(startPos, endPos);
        
        foreach (Vector2 v in route)
        {
            //o.GetComponent<MeshRenderer>().material.color = Color.yellow;
            aStarRoutePlanner.map[(int)v.x, (int)v.y].GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        aStarRoutePlanner.map[(int)startPos.x, (int)startPos.y].GetComponent<MeshRenderer>().material.color = Color.red;
        aStarRoutePlanner.map[(int)endPos.x, (int)endPos.y].GetComponent<MeshRenderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
