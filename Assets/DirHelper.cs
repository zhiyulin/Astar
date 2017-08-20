using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class DirHelper  {

    private static IList<Dir> allDir = new List<Dir>();

    static DirHelper()
    {
        DirHelper.allDir.Add(Dir.East);
        DirHelper.allDir.Add(Dir.North);
        DirHelper.allDir.Add(Dir.South);
        DirHelper.allDir.Add(Dir.West);
        DirHelper.allDir.Add(Dir.NorthEast);
        DirHelper.allDir.Add(Dir.NorthWest);
        DirHelper.allDir.Add(Dir.SouthEast);
        DirHelper.allDir.Add(Dir.SouthWest);
    }

    public static IList<Dir> GetAllDir()
    {
        return DirHelper.allDir;
    }
}
