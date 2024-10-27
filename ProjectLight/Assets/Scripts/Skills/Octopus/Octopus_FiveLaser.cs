using UnityEngine;

[CreateAssetMenu(fileName = "Octopus_FiveLaser", menuName = "Skill/Octopus/FiveLaser")]
public class Octopus_FiveLaser : LaserSkill
{
    public Octopus_FiveLaser_OneShot onShot;
    public Octopus_FiveLaser_ProtectiveLaser protectiveLaser;

    public void Init(StateMachine stateMachine)
    {
        onShot.Init(stateMachine);
    }
}