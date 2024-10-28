using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{

    [SerializeField]
    private int m_damage = 20;

    // TODO: 碰撞 LayerMask / Matrix 配置

    private void OnTriggerEnter2D(Collider2D hit)
    {
        BulletDamage damageable = hit?.GetComponent<BulletDamage>();
        if (damageable != null)
        {
            damageable.OnBulletHit(m_damage);
        }
    }
}
