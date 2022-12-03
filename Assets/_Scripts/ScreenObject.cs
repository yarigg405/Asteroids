//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class ScreenObject
//{
//    public const float FPS = 60f;
//    public const int maxX = 10000;
//    public const int maxY = 7500;


//    protected ArrayList points;
//    public ArrayList pointsTransformed;
//    protected Vector2 currLoc;

//    protected Vector2 speed;
//    protected float radians;


//    public Vector2 GetCurrLoc()
//    {
//        return currLoc;
//    }

//    public Vector2 GetSpeed()
//    {
//        return speed;
//    }

//    public float GetRadians()
//    {
//        return radians;
//    }


//    public ScreenObject(Vector2 location)
//    {
//        radians = 180f * Mathf.PI / 180f;
//        points = new ArrayList();
//        points.Capacity = 20;
//        pointsTransformed = new ArrayList();
//        pointsTransformed.Capacity = 20;
//        speed.x = 0;
//        speed.y = 0;
//        currLoc = location;

//        InitPoints();
//    }

//    public abstract void InitPoints();

//    public int AddPoint(Vector2 pt)
//    {
//        points.Add(pt);
//        return pointsTransformed.Add(pt);
//    }


//    protected void Rotate(float degrees)
//    {
//        float radiansAdjust = degrees * Mathf.Deg2Rad;
//        radians += radiansAdjust / FPS;
//        float sinVal = Mathf.Sin(radians);
//        float cosVal = Mathf.Cos(radians);


//        pointsTransformed.Clear();
//        Vector2 ptTransformed = Vector2.zero;
//        for (int i = 0; i < points.Count; i++)
//        {
//            Vector2 pt = ((Vector2)points[i]);
//            ptTransformed.x = (int)(pt.x * cosVal + pt.y * sinVal);
//            ptTransformed.y = (int)(pt.x * sinVal - pt.y * cosVal);
//            pointsTransformed.Add(ptTransformed);
//        }
//    }

//    public virtual bool Move()
//    {
//        currLoc.x += (int)speed.x;
//        currLoc.y += (int)speed.y;

//        if (currLoc.x < 0)
//            currLoc.x = maxX - 1;
//        if (currLoc.x >= maxX)
//            currLoc.x = 0;
//        if (currLoc.y < 0)
//            currLoc.y = maxY - 1;
//        if (currLoc.y >= maxY)
//            currLoc.y = 0;

//        return true;
//    }


//}
