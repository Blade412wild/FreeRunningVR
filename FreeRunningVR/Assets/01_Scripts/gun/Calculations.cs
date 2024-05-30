using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculations
{

    static bool CalculateIfObjectIsInArea(Transform center, float radius)
    {
        return true;
    }

    public static bool CalculateCapsuleCollision(Vector3 center, Transform target, float height, float radius)
    {
        
        float maxHeight = center.y + height / 2;
        float minHeight = center.y - height / 2;

        if(target.position.y < minHeight || target.position.y > maxHeight) return false;

        Vector2 centerV2 = new Vector2(center.x, center.z);
        Vector2 targetV2 = new Vector2(target.position.x, target.position.z);

        float horizontalDistance = Vector2.Distance(centerV2, targetV2);

        if (horizontalDistance > radius) return false;

        return true;
    }
}
