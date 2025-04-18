using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//snap directional vector to up, down, left or right, to prevent opponent from moving in any weird directions
public static class DirectionUtils
{
    public static Vector2 SnapToCardinal(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return new Vector2(Mathf.Sign(dir.x), 0f);
        }
        else
        {
            return new Vector2(0f, Mathf.Sign(dir.y));
        }
    }
}
