using System;
using UnityEngine;

public static class Extensions {
    public static float dotProduct(this Vector2 a, Vector2 b){
        return a.x * b.x + a.y * b.y;
    }

    public static float angle(this Vector2 a, Vector2 b){
        var dotProduct = a.dotProduct(b);
        var lenghtProduct = a.magnitude * b.magnitude;
        return Mathf.Acos(dotProduct/lenghtProduct) * Mathf.Rad2Deg;
    }

    public static Vector3 crossProduct(this Vector3 a, Vector3 b){
        //                                   powinno byc b.y
        return new Vector3(a.y * b.z - a.z * b.z, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
    }
}