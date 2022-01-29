using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extention
{
    public static Vector3 ToVector3(this Vector2 vector2, float z = 0) {
        var vector3 = (Vector3)vector2;
        vector3.z = z;
        return vector3;
    }

}
