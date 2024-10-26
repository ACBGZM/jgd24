using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 脚本挂在Box上
public class DropArea : MonoBehaviour
{
    [SerializeField]
    public GameObject dropPrefab;

    [Header("掉落范围")]
    public float outRadius = 5f;
    public float inRadius = 3f;
    public float angle = 90f;
    public float startAngle = 0f;
    private Vector2 center;

    public float minPopHeight = 0f;
    public float maxPopHeight = 5f;

    // [HideInInspector]
    private Vector2 dropPoint; //掉落位置

    void Start()
    {
        center = transform.position;
        // OnCreateDropItem();
    }

    public void OnCreateDropItem()
    {
        GameObject newDropItem = Instantiate(
            dropPrefab,
            transform.position,
            Quaternion.identity,
            this.transform
        );
        Vector2 dropPostion = CreateDropPosition()- new Vector2(transform.localScale.x, transform.localScale.y);


        newDropItem.GetComponent<ItemPopAnimation>().dropPoint = dropPostion;
        newDropItem.GetComponent<ItemPopAnimation>().popHeight = Random.Range(
            minPopHeight,
            maxPopHeight
        );
    }

    public Vector2 CreateDropPosition()
    {
        float dropRadius = Random.Range(inRadius, outRadius);
        float dropAngle = Random.Range(startAngle, angle + startAngle);

        Vector2 dropPoint =
            new Vector2(Mathf.Cos(dropAngle * Mathf.Deg2Rad), Mathf.Sin(dropAngle * Mathf.Deg2Rad))
            * dropRadius ; 
        return dropPoint;
    }

    // 区域绘制
    private void OnDrawGizmos()
    {
        center = transform.position;
        Gizmos.color = Color.yellow;

        Vector2 startInPoint = center;
        Vector2 startOutPoint = center;

        Vector2 curInPoint;
        Vector2 curOutPoint;

        int segments = 32;
        float segmentAngle = angle / segments;
        for (int i = 0; i < segments; i++)
        {
            float curAngle = startAngle + i * segmentAngle;

            curInPoint =
                new Vector2(
                    Mathf.Cos(curAngle * Mathf.Deg2Rad),
                    Mathf.Sin(curAngle * Mathf.Deg2Rad)
                )
                    * inRadius
                    * transform.localScale
                + center;

            curOutPoint =
                new Vector2(
                    Mathf.Cos(curAngle * Mathf.Deg2Rad),
                    Mathf.Sin(curAngle * Mathf.Deg2Rad)
                )
                    * outRadius
                    * transform.localScale
                + center;

            if (i == 0)
            {
                Gizmos.DrawLine(startOutPoint, curOutPoint);
            }
            else
            {
                Gizmos.DrawLine(startOutPoint, curOutPoint);
                Gizmos.DrawLine(startInPoint, curInPoint);
            }

            startOutPoint = curOutPoint;
            startInPoint = curInPoint;
        }
        Gizmos.DrawLine(startOutPoint, center);
    }
}
