

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionAttack : MonoBehaviour
{

    [SerializeField]
    private int m_damage = 20;

    // TODO: 碰撞 LayerMask / Matrix 配置

    private void OnTriggerEnter2D(Collider2D hit)
    {
        BossDamage damageable = hit?.GetComponent<BossDamage>();
        if (damageable != null)
        {
            damageable.OnBossHit(m_damage);
        }
    }
}

