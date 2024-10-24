using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletSize;
    public float bulletLifeTime;
    public float damageRate;

    private float m_currentLifeTime;

    void Awake()
    {
        m_currentLifeTime = 0;
    }

    void Update()
    {
        m_currentLifeTime += Time.deltaTime;
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        if (m_currentLifeTime >= bulletLifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetBulletStat(BulletLauncherStat launcherStat)
    {
        bulletSpeed = launcherStat.BulletSpeed;
        bulletSize = launcherStat.BulletSize;
        bulletLifeTime = launcherStat.BulletLifeTime;
        damageRate = launcherStat.DamageRate;
    }
}