using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Laser Skill", menuName = "Skill/Laser Skill")]
public class LaserSkill : Skill
{
    public GameObject launcherPrefab;
    public List<LaserLauncherStat> launcherStats;

    public bool aimToPlayer = false;
    public Vector2 originalPos;
    public Vector2 playerPos;

    public float duration;

    public void Init(Vector2 originalPos, Vector2 playerPos)
    {
        this.originalPos = originalPos;
        this.playerPos = playerPos;
    }

    public void UpdateOriginalPos(Vector2 originalPos)
    {
        this.originalPos = originalPos;
    }
    public void UpdatePlayerPos(Vector2 playerPos)
    {
        this.playerPos = playerPos;
    }

    public void Cast()
    {
        foreach (LaserLauncherStat launcherStat in launcherStats)
        {
            Vector2 unitVector = aimToPlayer
            ? playerPos - originalPos
            : Vector2.up;
            Vector2 startDirection = MathTool.RotateVector2(unitVector, Mathf.Deg2Rad * launcherStat.InitLaunchAngle);
            Vector2 startPosition = originalPos + startDirection * launcherStat.InstantiateDistanceOffset;
            GameObject launcher = Object.Instantiate(launcherPrefab, originalPos,
                Quaternion.identity);
            launcherStat.AimToPlayer = aimToPlayer;
            launcher.GetComponent<Laser>().SetLaunchParameter(startPosition, startDirection);
            launcher.GetComponent<Laser>().SetLaserStat(launcherStat);
        }
    }
}