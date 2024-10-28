using UnityEngine;
using UnityEngine.PlayerLoop;

public class LaserLauncher : MonoBehaviour
{
    public LaserLauncherStat launcherStat;

    public float currentRotateAngle;
    public float currentRotateTime;

    void Awake()
    {
        Init(launcherStat);
    }

    void Start()
    {
        Launch();
    }

    void Update()
    {
        currentRotateTime += Time.deltaTime;
    }

    public void Init(LaserLauncherStat launcherStat)
    {
        this.launcherStat = launcherStat;
        currentRotateAngle = 0;
        currentRotateTime = 0;
    }

    void Launch()
    {
        Vector2 direction = Vector2.up;
        direction = MathTool.RotateVector2(direction, launcherStat.InitLaunchAngle);
        Vector2 StartPosition = transform.position + (Vector3)direction * launcherStat.InstantiateDistanceOffset;
        GameObject laserObject = Instantiate(launcherStat.LaserPrefab, StartPosition, Quaternion.identity);
    }

}