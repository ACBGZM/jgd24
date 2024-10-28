using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMirrorBomb : Bomb
{
    // 贴图更换
    public List<Sprite> spriteList; // 0: 初始 1:Broken
    private SpriteRenderer m_SpriteRenderer;
    private BombMirrorShell mirrorShell;

    public void OnEnable()
    {
         MirrorBrokenEvent.ReportMirrorBroken += OnMirrorBroken;
    }
    public void OnDisable()
    {
         MirrorBrokenEvent.ReportMirrorBroken -= OnMirrorBroken;
    }



    void Start()
    {
        base.Start();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = spriteList[0];

        mirrorShell = GetComponentInChildren<BombMirrorShell>();
    }

    void Update()
    {
        
    }


    private void OnMirrorBroken()
    {
         m_SpriteRenderer.sprite = spriteList[1];
         if(mirrorShell != null)
         {
           Destroy(mirrorShell.gameObject);
         }

    }



}
