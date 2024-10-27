using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Octopus/FiveLaser_OneShot", fileName = "Octopus_FiveLaser_OneShot")]
public class Octopus_FiveLaser_OneShot : LaserSkill
{
    public float instantiateDistanceOffset;
    public OctopusStateMachine stateMachine;

    public void Init(StateMachine stateMachine)
    {
        this.stateMachine = (OctopusStateMachine)stateMachine;
    }

    public void Cast()
    {
        Vector2 laserDirection = aimToPlayer
            ? (playerPos - originalPos).normalized
            : Vector2.up;
        Vector2 laserStartPosition = originalPos + laserDirection * instantiateDistanceOffset;

        GameObject jellyfish = Object.Instantiate(jellyfishPrefab, laserStartPosition, Quaternion.identity);
        jellyfish.GetComponent<Jellyfish>().Init(stateMachine);
        jellyfish.GetComponent<Jellyfish>().LaserDirection = laserDirection;
        stateMachine.jellyfishes.Add(jellyfish.GetComponent<Jellyfish>());
    }
}