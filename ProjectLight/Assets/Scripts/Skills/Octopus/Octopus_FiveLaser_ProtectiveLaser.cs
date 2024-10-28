using UnityEngine;

[CreateAssetMenu(fileName = "Octopus_FiveLaser_ProtectiveLaser", menuName = "Skill/Octopus/FiveLaser_ProtectiveLaser")]
public class Octopus_FiveLaser_ProtectiveLaser : LaserSkill
{
    public float laserOffset;
    public OctopusStateMachine stateMachine;
    public GameObject LayserLauncher;

    public void Init(StateMachine stateMachine)
    {
        this.stateMachine = (OctopusStateMachine)stateMachine;
    }

    public void Cast()
    {
        GameObject protectiveJellyfish1 = GameObject.Instantiate(jellyfishPrefab, playerPos + new Vector2(1, 1).normalized * laserOffset, Quaternion.identity);
        protectiveJellyfish1.GetComponent<Jellyfish>().LaserDirection = Vector2.down;
        protectiveJellyfish1.GetComponent<Jellyfish>().LaserLauncherPrefab = LayserLauncher;
        stateMachine.jellyfishes.Add(protectiveJellyfish1.GetComponent<Jellyfish>());
        GameObject protectiveJellyfish2 = GameObject.Instantiate(jellyfishPrefab, playerPos + new Vector2(-1, 1).normalized * laserOffset, Quaternion.identity);
        protectiveJellyfish2.GetComponent<Jellyfish>().LaserDirection = Vector2.right;
        protectiveJellyfish2.GetComponent<Jellyfish>().LaserLauncherPrefab = LayserLauncher;
        stateMachine.jellyfishes.Add(protectiveJellyfish2.GetComponent<Jellyfish>());
        GameObject protectiveJellyfish3 = GameObject.Instantiate(jellyfishPrefab, playerPos + new Vector2(1, -1).normalized * laserOffset, Quaternion.identity);
        protectiveJellyfish3.GetComponent<Jellyfish>().LaserDirection = Vector2.left;
        protectiveJellyfish3.GetComponent<Jellyfish>().LaserLauncherPrefab = LayserLauncher;
        stateMachine.jellyfishes.Add(protectiveJellyfish3.GetComponent<Jellyfish>());
        GameObject protectiveJellyfish4 = GameObject.Instantiate(jellyfishPrefab, playerPos + new Vector2(-1, -1).normalized * laserOffset, Quaternion.identity);
        protectiveJellyfish4.GetComponent<Jellyfish>().LaserDirection = Vector2.up;
        protectiveJellyfish4.GetComponent<Jellyfish>().LaserLauncherPrefab = LayserLauncher;
        stateMachine.jellyfishes.Add(protectiveJellyfish4.GetComponent<Jellyfish>());
        
        /*
        GameObject protectiveLauncher1 = Object.Instantiate(launcherPrefab, playerPos + new Vector2(1, 1).normalized * laserOffset, Quaternion.identity);
        protectiveLauncher1.GetComponent<Laser>().SetLaunchParameter(protectiveLauncher1.transform.position, Vector2.down);
        protectiveLauncher1.GetComponent<Laser>().SetLaserStat(ProtectiveLaserLauncherStat);

        GameObject protectiveLauncher2 = Object.Instantiate(launcherPrefab, playerPos + new Vector2(-1, 1).normalized * laserOffset, Quaternion.identity);
        protectiveLauncher2.GetComponent<Laser>().SetLaunchParameter(protectiveLauncher2.transform.position, Vector2.right);
        protectiveLauncher2.GetComponent<Laser>().SetLaserStat(ProtectiveLaserLauncherStat);

        GameObject protectiveLauncher3 = Object.Instantiate(launcherPrefab, playerPos + new Vector2(1, -1).normalized * laserOffset, Quaternion.identity);
        protectiveLauncher3.GetComponent<Laser>().SetLaunchParameter(protectiveLauncher3.transform.position, Vector2.left);
        protectiveLauncher3.GetComponent<Laser>().SetLaserStat(ProtectiveLaserLauncherStat);

        GameObject protectiveLauncher4 = Object.Instantiate(launcherPrefab, playerPos + new Vector2(-1, -1).normalized * laserOffset, Quaternion.identity);
        protectiveLauncher4.GetComponent<Laser>().SetLaunchParameter(protectiveLauncher4.transform.position, Vector2.up);
        protectiveLauncher4.GetComponent<Laser>().SetLaserStat(ProtectiveLaserLauncherStat);


        foreach (LaserLauncherStat launcherStat in launcherStats)
        {
            Vector2 unitVector = aimToPlayer
            ? playerPos - originalPos
            : Vector2.up;
            Vector2 startDirection = MathTool.RotateVector2(unitVector, Mathf.Deg2Rad * launcherStat.InitLaunchAngle);
            Vector2 startPosition = originalPos + startDirection * launcherStat.InstantiateDistanceOffset;
            GameObject launcher = Object.Instantiate(launcherPrefab, startPosition,
                Quaternion.identity);
            launcherStat.AimToPlayer = aimToPlayer;
            launcher.GetComponent<Laser>().SetLaunchParameter(startPosition, startDirection);
            launcher.GetComponent<Laser>().SetLaserStat(launcherStat);
        }
        */
    }
}