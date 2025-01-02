using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class LunarMath
{

    // Gives angle phi between two Vectors in radians using atan2.
    public static float VectorAngle(Vector2 from, Vector2 to)
    {
        return Mathf.Atan2(from.y - to.y, from.x - to.x);
    }
}
