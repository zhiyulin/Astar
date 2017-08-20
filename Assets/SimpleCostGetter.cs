using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCostGetter : ICostGetter {

    public int GetCost(Vector2 curLocation, Dir moveDir)
    {
        if (moveDir == Dir.NotSet)
            return 0;
        if (moveDir == Dir.East || moveDir == Dir.West || moveDir == Dir.South || moveDir == Dir.North)
            return 10;

        return 14;
    }
}
