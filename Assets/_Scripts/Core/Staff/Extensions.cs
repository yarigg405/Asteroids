using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2 GetRandomCoordinatesAroundPoint(this Vector2 originalPoint, float radius,
            bool pointOnRadiusLine = false)
    {
        float angle = Random.Range(0, 360);
        float lenght = pointOnRadiusLine ? radius :
            (Random.Range(0, radius));

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * lenght;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * lenght;


        Vector2 result = new Vector2(originalPoint.x + x, originalPoint.y + y);


        return result;
    }


    public static T GetRandomItem<T>(this List<T> list)
    {
        return list.Count < 1 ? default(T) : list[Random.Range(0, list.Count)];
    }
}