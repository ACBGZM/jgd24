using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawnDetection : MonoBehaviour
{
    public bool canRespawn = true;

    // [SerializeField] private string[] box_layermask_list;
    // [SerializeField] private LayerMask box_layermask;

   

    //private LineRenderer[] m_line_renderers;
    private void Awake()
    {
        // box_layermask = LayerMask.GetMask(box_layermask_list);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRespawn = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRespawn = true;
        }
    }
}
