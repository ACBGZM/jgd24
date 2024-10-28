using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Skill", menuName = "Skill/Bullet Skill")]
public class BulletSkill : Skill
{
    public GameObject launcherPrefab;
    public List<BulletLauncherStat> launcherStats;
    
    public string animationClipName;

    public bool aimToPlayer = false;

    public Vector2 originalPos;
    public Vector2 playerPos;

    public void Init(Vector2 originalPos, Vector2 playerPos)
    {
        this.originalPos = originalPos;
        this.playerPos = playerPos;
    }

    public void UpdatePlayerPos(Vector2 playerPos)
    {
        this.playerPos = playerPos;
    }
    
    public void Cast()
    {
        foreach (BulletLauncherStat launcherStat in launcherStats)
        {
            Quaternion bulletRotation = aimToPlayer
                ? Quaternion.LookRotation(Vector3.forward,
                    playerPos - originalPos)
                : Quaternion.identity;
            GameObject launcher = Object.Instantiate(launcherPrefab, originalPos,
                bulletRotation);
            launcher.GetComponent<BulletLauncher>().Init(launcherStat);
        }
    }
}