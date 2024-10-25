using UnityEngine;

[CreateAssetMenu(fileName = "New Octpus_RowLaser", menuName = "Skill/Octopus/RowLaser")]
public class Octopus_RowLaser : LaserSkill
{
    public float laserGap;

    public void Cast()
    {
        foreach (LaserLauncherStat launcherStat in launcherStats)
        {
            Vector2 unitVector = aimToPlayer
            ? playerPos - originalPos
            : Vector2.up;
            Vector2 startDirection = MathTool.RotateVector2(unitVector, Mathf.Deg2Rad * launcherStat.InitLaunchAngle);
            Vector2 startPosition = originalPos + startDirection * launcherStat.InstantiateDistanceOffset;

            Vector3 verticalVector = Vector3.Cross(unitVector, Vector3.forward).normalized;
            // 一号激光
            GameObject launcher = Object.Instantiate(launcherPrefab, originalPos,
                Quaternion.identity);
            launcherStat.AimToPlayer = aimToPlayer;
            launcher.GetComponent<Laser>().SetLaunchParameter(startPosition, startDirection);
            launcher.GetComponent<Laser>().SetLaserStat(launcherStat);

            launcherStat.AimToPlayer = false;
            GameObject launcher2 = Object.Instantiate(launcherPrefab, originalPos + (Vector2)verticalVector * laserGap, Quaternion.identity);
            launcher2.GetComponent<Laser>().SetLaunchParameter(originalPos + (Vector2)verticalVector * laserGap, startDirection);
            launcher2.GetComponent<Laser>().SetLaserStat(launcherStat);

            GameObject launcher3 = Object.Instantiate(launcherPrefab, originalPos + (Vector2)verticalVector * laserGap * 2, Quaternion.identity);
            launcher3.GetComponent<Laser>().SetLaunchParameter(originalPos + (Vector2)verticalVector * laserGap * 2, startDirection);
            launcher3.GetComponent<Laser>().SetLaserStat(launcherStat);

            GameObject launcher4 = Object.Instantiate(launcherPrefab, originalPos + (Vector2)verticalVector * laserGap * -1, Quaternion.identity);
            launcher4.GetComponent<Laser>().SetLaunchParameter(startPosition + (Vector2)verticalVector * laserGap * -1, startDirection);
            launcher4.GetComponent<Laser>().SetLaserStat(launcherStat);

            GameObject launcher5 = Object.Instantiate(launcherPrefab, originalPos + (Vector2)verticalVector * laserGap * -2, Quaternion.identity);
            launcher5.GetComponent<Laser>().SetLaunchParameter(startPosition + (Vector2)verticalVector * laserGap * -2, startDirection);
            launcher5.GetComponent<Laser>().SetLaserStat(launcherStat);
        }
    }
}