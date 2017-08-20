using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryHelper {

    public static Vector2 GetAdjacentPoint(Vector2 cur, Dir dir)
    {
        switch (dir)
        {
            case Dir.North:
                return new Vector2(cur.x, cur.y - 1);
            case Dir.South:
                return new Vector2(cur.x, cur.y + 1);
            case Dir.East:
                return new Vector2(cur.x + 1, cur.y);
            case Dir.West:
                return new Vector2(cur.x - 1, cur.y);
            case Dir.NorthEast:
                return new Vector2(cur.x + 1, cur.y - 1);
            case Dir.NorthWest:
                return new Vector2(cur.x - 1, cur.y - 1);
            case Dir.SouthEast:
                return new Vector2(cur.x + 1, cur.y + 1);
            case Dir.SouthWest:
                return new Vector2(cur.x - 1, cur.y + 1);
            default:
                return cur;
        }
    }
}
