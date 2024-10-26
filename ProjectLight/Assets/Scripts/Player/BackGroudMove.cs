using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroudMove : MonoBehaviour
{
    public GameObject player;
    public Vector2 originPosition ;
    public Vector2 deltaPosition ;

    public float moveSpeed = -1.0f;


    void Start()
    {
        originPosition = Vector2.zero;
        deltaPosition = originPosition;
    }

    
    void Update()
    {
        deltaPosition = player.transform.position;
        transform.position = moveSpeed * deltaPosition;
    }
}
