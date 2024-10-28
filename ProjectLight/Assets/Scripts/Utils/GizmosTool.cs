using UnityEngine;

public static class GizmosTool
{
    /// <summary>
    /// 绘制扇形
    /// </summary>
    /// <param name="origin">扇形中心点</param>
    /// <param name="direction">扇形方向</param>
    /// <param name="radius">扇形半径</param>
    /// <param name="angle">扇形角度</param>
    /// <param name="axis">扇形旋转轴</param>
    public static void DrawWireSemicircle(Vector3 origin, Vector3 direction, float radius, int angle)
    {
        DrawWireSemicircle(origin, direction, radius, angle, Vector3.up);
    }
    public static void DrawWireSemicircle(Vector3 origin, Vector3 direction, float radius, int angle, Vector3 axis)
    {
        Vector3 leftDir = Quaternion.AngleAxis(-angle/2, axis) * direction;
        Vector3 rightDir = Quaternion.AngleAxis(angle/2, axis) * direction;

        Vector3 currentP = origin + leftDir * radius;
        Vector3 oldP;

        if (angle!=360)
        {
            Gizmos.DrawLine(origin, currentP);
        }
        for (int i = 0; i < angle / 10; i ++)
        {
            Vector3 dir = Quaternion.AngleAxis(10 * i, axis) * leftDir;
            oldP = currentP;
            currentP = origin + dir * radius;
            Gizmos.DrawLine(oldP, currentP);
        }
        oldP = currentP;
        currentP = origin + rightDir * radius;
        Gizmos.DrawLine(oldP, currentP);
        if (angle != 360)
        {
            Gizmos.DrawLine(currentP, origin);
        }
    }

    public static void DrawWireSemicircle2D(Vector2 origin, Vector2 direction, float radius, int angle)
    {
        DrawWireSemicircle(origin, direction, radius, angle, Vector3.forward);
    }
}