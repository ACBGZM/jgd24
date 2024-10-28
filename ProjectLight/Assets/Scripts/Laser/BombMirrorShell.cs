using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMirrorShell : MonoBehaviour, LaserDamage
{
    // 炸弹镜面壳
    // 受到激光伤害降低生存时长
    [SerializeField]
    public float lifeTimeScond = 3.0f; // todo: mirrorShell破坏程度反馈


    private float lastLifeTime;
    private float brokenSpeed = 1.0f;

    // public GameObject MirrorBrokenAnimPrefab;
    
    
    public void Start()
    {
        lastLifeTime = lifeTimeScond;
    }

    // LaserDamage接口
    public void OnLaserHit(int damage)
    {
        lastLifeTime -= Time.deltaTime * brokenSpeed;
        if (lastLifeTime < 0)
        {
            MirrorDestroy();
        }
    }

    private void MirrorDestroy()
    {   
        // GameObject newMirrirBrokenAnim =  Instantiate(MirrorBrokenAnimPrefab, transform.position, Quaternion.identity);
        MirrorBrokenEvent.CallReportMirrorBroken();
        // Destroy(gameObject); // 销毁镜面
    }
}
