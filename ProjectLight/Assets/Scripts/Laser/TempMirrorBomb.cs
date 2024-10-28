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
    // private Animator m_animator = null;
    public new Animator child_Animator 
    {
        get { return m_animator; }
        set { m_animator = value; }
    }

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
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_SpriteRenderer.sprite = spriteList[0];

        mirrorShell = GetComponentInChildren<BombMirrorShell>();
        child_Animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        
    }


    private void OnMirrorBroken()
    {   
       
        child_Animator.SetTrigger("Broke");
        m_SpriteRenderer.sprite = spriteList[1];

        if(mirrorShell != null)
        {
            Destroy(mirrorShell.gameObject);
        }

    }



}
