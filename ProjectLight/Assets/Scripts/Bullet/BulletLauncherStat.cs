using UnityEngine;

[CreateAssetMenu(menuName = "Launcher/BulletLauncherStat", fileName = "New BulletLauncherStat")]
public class BulletLauncherStat : ScriptableObject
{
    public bool Instantiated = false;

    [Header("Bullet Parameters")]
    
    [SerializeField]
    private GameObject bulletPrefab;
    public GameObject BulletPrefab => bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 5;
    public float BulletSpeed => bulletSpeed;

    [SerializeField]
    private float bulletSize = 1;
    public float BulletSize => bulletSize;
    
    [SerializeField]
    private float bulletLifeTime = 5;
    public float BulletLifeTime => bulletLifeTime;

    [Header("Launcher Parameters")]

    [SerializeField]
    private float initLaunchAngle = 0;
    public float InitLaunchAngle => initLaunchAngle;

    [SerializeField]
    private int bulletQuantity = 1;
    public int BulletQuantity => bulletQuantity;

    [SerializeField]
    private float intervalLaunchTime = 0;
    public float IntervalLaunchTime => intervalLaunchTime;
    
    [SerializeField]
    private float intervalLaunchAngle = 0;
    public float IntervalLaunchAngle => intervalLaunchAngle;

    [SerializeField]
    private float damageRate = 1;
    public float DamageRate => damageRate;
}