using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    static GameObject movingWall;
    static GameObject ball;


    public static float getMovingWallHeight()
    {
        findObjectsIfNeccessary();
        return movingWall.transform.localScale.y;
    }

    public static float getMovingWallWidth()
    {
        findObjectsIfNeccessary();
        return movingWall.transform.localScale.x;
    }


    private static void findObjectsIfNeccessary()
    {
        if (movingWall == null)
            movingWall = GameObject.Find("WallLeft");
        if (ball == null)
            GameObject.Find("Ball");
    }
}
