using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public const string Ball = "Ball";
    public const string CurrentRecord = "CurrentRecord";

    public static List<Color> BallBounceColors = new List<Color> { Color.black, new Color(0.3f, 0.3f, 0.3f), new Color(0.75f, 0.75f, 0.75f), Color.white };

    public static int maxGroundTouches = 3;

    public static int TotalBalls = 40;
}
