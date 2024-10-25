using UnityEngine;

[CreateAssetMenu(menuName = "Launcher/LaserLauncherStat", fileName = "New LaserLauncherStat")]
public class LaserLauncherStat : ScriptableObject
{
    [Header("Laser Parameters")]
    [SerializeField]
    private GameObject laserPrefab;
    public GameObject LaserPrefab => laserPrefab;
    [SerializeField]
    private float laserWidth = 0.1f;
    public float LaserWidth => laserWidth;
    [SerializeField]
    private float laserLifeTime = 5;
    public float LaserLifeTime => laserLifeTime;

    [Header("Launcher Parameters")]
    [SerializeField]
    private float instantiateDistanceOffset = 0;
    public float InstantiateDistanceOffset => instantiateDistanceOffset;
    [SerializeField]
    private float initLaunchAngle = 0;
    public float InitLaunchAngle => initLaunchAngle;
    [SerializeField]
    private float rotateAngle = 0;
    public float RotateAngle => rotateAngle;
    [SerializeField]
    private float rotateTime = 0;
    public float RotateTime => rotateTime;

    [SerializeField]
    private bool aimToPlayer = false;
    public bool AimToPlayer { get; set; }

    public void SetInitLaunchAngle(float angle)
    {
        initLaunchAngle = angle;
    }
}