using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    private static int x = 10;
    private static int y = 10;
    private static float bombsPercentage = 0.15f;
    public static int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value > 0 ? value : 10;
        }
    }
    public static int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value > 0 ? value : 10;
        }
    }
    public static int Bombs
    {
        get
        {
            int bombs = (int)((bombsPercentage) * (X + 1) * (Y + 1));
            return bombs == 0 ? 15 : bombs;
        }

    }

    public static float BombsPercentage { get => bombsPercentage; set => bombsPercentage = value; }
}
