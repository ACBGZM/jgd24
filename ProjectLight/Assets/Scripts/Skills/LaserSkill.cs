using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser Skill", menuName = "Skill/Laser Skill")]
public class LaserSkill : Skill
{
    public GameObject launcherPrefab;
    public List<LaserLauncherStat> launcherStats;

    public bool aimToPlayer = false;
}