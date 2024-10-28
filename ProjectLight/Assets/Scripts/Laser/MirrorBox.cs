using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBox : Box,LaserDamage
{
    [SerializeField]
    public string m_tag = "Mirror";

    [SerializeField]
    public string m_layer = "Mirror";

    public override void  Start()
    {   
        base.Start();
        TagInit(m_tag, m_layer);
    }


    public void OnLaserHit(int damage)
    {   
        Health -= 1; // 具体伤害由damage折算
        if (Health <= 0)
        {
            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public override void BoxRespawn()
    {      
        BoxInit();
        TagInit(m_tag, m_layer);
    }

    private void TagInit(string tag, string layer)
    {
        gameObject.tag = tag;
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
}
