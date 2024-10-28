using System;
using UnityEngine;

public class MathTool
{
    /// <summary>
    /// 计算圆外一点到圆周上切线的坐标
    /// </summary>
    /// <param name="center">圆心坐标</param>
    /// <param name="radius">圆周半径</param>
    /// <param name="point">点坐标</param>
    public static (Vector2, Vector2) CalculateTangent(Vector2 center, float radius, Vector2 point)
    {
        Vector2 pc = point - center;
        float distance = pc.magnitude;
        
        float tangentLineLength = Mathf.Sqrt(distance * distance - radius * radius);

        if (distance <= radius)
        {
            Debug.LogError("Point is inside the circle");
            return (Vector2.zero, Vector2.zero);
        }

        float ux = (center.x - point.x) / distance;
        float uy = (center.y - point.y) / distance;

        float angle = Mathf.Asin(radius / distance);

        float q1x = ux * Mathf.Cos(angle) - uy * Mathf.Sin(angle);
        float q1y = ux * Mathf.Sin(angle) + uy * Mathf.Cos(angle);
        float q2x = ux * Mathf.Cos(-angle) - uy * Mathf.Sin(-angle);
        float q2y = ux * Mathf.Sin(-angle) + uy * Mathf.Cos(-angle);
        
        q1x = q1x * tangentLineLength + point.x;
        q1y = q1y * tangentLineLength + point.y;
        q2x = q2x * tangentLineLength + point.x;
        q2y = q2y * tangentLineLength + point.y;

        return (new Vector2(q1x, q1y), new Vector2(q2x, q2y));
    }

    /// <summary>
    /// 平面内一点到圆心连线与圆周的交点坐标
    /// </summary>
    /// <param name="center">圆心坐标</param>
    /// <param name="radius">圆周半径</param>
    /// <param name="point">点坐标</param>
    public static Vector2 CalculateIntersectionPointWithCircle(Vector2 center, float radius, Vector2 point)
    {
        Vector2 cp = point - center;
        Vector2 unityCP = cp.normalized;
        
        Vector2 vectorFromCenterToIntersectionPoint = unityCP * radius;
        Vector2 intersectionPoint = center + vectorFromCenterToIntersectionPoint;
        return intersectionPoint;
    }

    /// <summary>
    /// 计算圆外一点到圆周上切线夹角
    /// </summary>
    /// <param name="center">圆心坐标</param>
    /// <param name="radius">圆周半径</param>
    /// <param name="point">点坐标</param>
    /// <returns></returns>
    public static (float, float) CalculateAngleRange(Vector2 center, float radius, Vector2 point)
    {
        float distance = (point - center).magnitude;
        (float, float) angleRange = new(-Mathf.Asin(radius / distance), Mathf.Asin(radius / distance));
        return angleRange;
    }

    /// <summary>
    /// 计算圆外一点到圆周上一点的距离
    /// </summary>
    /// <param name="center">圆心坐标</param>
    /// <param name="radius">圆周半径</param>
    /// <param name="point">点坐标</param>
    /// <param name="angle">所求线段与圆心到点连线的夹角</param>
    /// <returns>返回较短距离</returns>
    public static float CalculateDistanceToCircle(Vector2 center, float radius, Vector2 point, float angle)
    {
        Vector2 pc = center - point;
        float distance = pc.magnitude;

        (float, float) distanceToCircle =
            SolveQuadraticEquation(1, -2 * distance * Mathf.Cos(angle), distance * distance - radius * radius);

        return distanceToCircle.Item1;
    }
    
    // 解一元二次方程
    public static (float, float) SolveQuadraticEquation(float a, float b, float c)
    {
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            Debug.LogError("No real root");
        }

        float root1 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        float root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        return (root1, root2);
    }

    // 旋转二维向量
    public static Vector2 RotateVector2(Vector2 vector, float angle)
    {
        float x = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float y = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
        return new Vector2(x, y);
    }
    
    
    
    
    
}