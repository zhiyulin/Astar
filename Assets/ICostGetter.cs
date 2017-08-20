using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Dir{
    NotSet = 0,
    North = 1,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

public interface ICostGetter  {

    int GetCost(Vector2 curNodeLocation, Dir moveDir);
}
