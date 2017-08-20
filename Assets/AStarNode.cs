using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode  {


    public AStarNode(Vector2 loc,AStarNode previous, int _costG, int _costH)
    {
        this.location = loc;
        this.previousNode = previous;
        this.costG = _costG;
        this.costH = _costH;
    }

    private Vector2 location = new Vector2(0, 0);
    public Vector2 Location
    {
        get { return location; }
    }

    private AStarNode previousNode = null;

    public AStarNode PreviousNode
    {
        get { return previousNode; }
    }

    public int CostF
    {
        get
        {
            return this.costG + this.CostH;
        }
    }
    private int costG = 0;
    public int CostG
    {
        get { return costG; }
    }
    private int costH = 0;
    public int CostH
    {
        get { return costH; }
    }

    public void ResetPreviousNode(AStarNode previous, int _costG)
    {
        this.previousNode = previous;
        this.costG = _costG;
    }

    public override string ToString()
    {
        return this.location.ToString();
    }
}
