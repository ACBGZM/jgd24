using UnityEngine;

[CreateAssetMenu(fileName = "Octopus_RotateLaser", menuName = "Skill/Octopus/RotateLaser")]
public class Octopus_RotateLaser : LaserSkill
{
    public float rotateTime;
    public float rotateAngle;

    public float instantiateDistanceOffset;
    public OctopusStateMachine stateMachine;

    public  void Init(StateMachine stateMachine)
    {
        this.stateMachine = (OctopusStateMachine)stateMachine;
    }

    public void Cast()
    {
        Vector2 laserDirection = aimToPlayer
            ? (playerPos - originalPos).normalized
            : Vector2.up;
        Vector2 laserStartPosition = originalPos + laserDirection * instantiateDistanceOffset;

        GameObject jellyfish1 = Object.Instantiate(jellyfishPrefab, laserStartPosition, Quaternion.identity);
        jellyfish1.GetComponent<Jellyfish>().Init(stateMachine);
        jellyfish1.GetComponent<Jellyfish>().LaserDirection = laserDirection;
        jellyfish1.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
        jellyfish1.GetComponent<Jellyfish>().rotateTime = rotateTime;
        stateMachine.jellyfishes.Add(jellyfish1.GetComponent<Jellyfish>());

        Vector2 laserDirection2 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 90);
        Vector2 laserStartPosition2 = originalPos + laserDirection2 * instantiateDistanceOffset;
        GameObject jellyfish2 = Object.Instantiate(jellyfishPrefab, laserStartPosition2, Quaternion.identity);
        jellyfish2.GetComponent<Jellyfish>().Init(stateMachine);
        jellyfish2.GetComponent<Jellyfish>().LaserDirection = laserDirection2;
        jellyfish2.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
        jellyfish2.GetComponent<Jellyfish>().rotateTime = rotateTime;
        stateMachine.jellyfishes.Add(jellyfish2.GetComponent<Jellyfish>());

        Vector2 laserDirection3 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 180);;
        Vector2 laserStartPosition3 = originalPos + laserDirection3 * instantiateDistanceOffset;
        GameObject jellyfish3 = Object.Instantiate(jellyfishPrefab, laserStartPosition3, Quaternion.identity);
        jellyfish3.GetComponent<Jellyfish>().Init(stateMachine);
        jellyfish3.GetComponent<Jellyfish>().LaserDirection = laserDirection3;
        jellyfish3.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
        jellyfish3.GetComponent<Jellyfish>().rotateTime = rotateTime;
        stateMachine.jellyfishes.Add(jellyfish3.GetComponent<Jellyfish>());

        Vector2 laserDirection4 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 270);;
        Vector2 laserStartPosition4 = originalPos + laserDirection4 * instantiateDistanceOffset;
        GameObject jellyfish4 = Object.Instantiate(jellyfishPrefab, laserStartPosition4, Quaternion.identity);
        jellyfish4.GetComponent<Jellyfish>().Init(stateMachine);
        jellyfish4.GetComponent<Jellyfish>().LaserDirection = laserDirection4;
        jellyfish4.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
        jellyfish4.GetComponent<Jellyfish>().rotateTime = rotateTime;
        stateMachine.jellyfishes.Add(jellyfish4.GetComponent<Jellyfish>());

        if(stateMachine.Phase2)
        {
            Vector2 laserDirection5 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 45);
            Vector2 laserStartPosition5 = originalPos + laserDirection5 * instantiateDistanceOffset;
            GameObject jellyfish5 = Object.Instantiate(jellyfishPrefab, laserStartPosition5, Quaternion.identity);
            jellyfish5.GetComponent<Jellyfish>().Init(stateMachine);
            jellyfish5.GetComponent<Jellyfish>().LaserDirection = laserDirection5;
            jellyfish5.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
            jellyfish5.GetComponent<Jellyfish>().rotateTime = rotateTime;
            stateMachine.jellyfishes.Add(jellyfish5.GetComponent<Jellyfish>());

            Vector2 laserDirection6 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 135);
            Vector2 laserStartPosition6 = originalPos + laserDirection6 * instantiateDistanceOffset;
            GameObject jellyfish6 = Object.Instantiate(jellyfishPrefab, laserStartPosition6, Quaternion.identity);
            jellyfish6.GetComponent<Jellyfish>().Init(stateMachine);
            jellyfish6.GetComponent<Jellyfish>().LaserDirection = laserDirection6;
            jellyfish6.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
            jellyfish6.GetComponent<Jellyfish>().rotateTime = rotateTime;
            stateMachine.jellyfishes.Add(jellyfish6.GetComponent<Jellyfish>());

            Vector2 laserDirection7 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 225);
            Vector2 laserStartPosition7 = originalPos + laserDirection7 * instantiateDistanceOffset;
            GameObject jellyfish7 = Object.Instantiate(jellyfishPrefab, laserStartPosition7, Quaternion.identity);
            jellyfish7.GetComponent<Jellyfish>().Init(stateMachine);
            jellyfish7.GetComponent<Jellyfish>().LaserDirection = laserDirection7;
            jellyfish7.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
            jellyfish7.GetComponent<Jellyfish>().rotateTime = rotateTime;
            stateMachine.jellyfishes.Add(jellyfish7.GetComponent<Jellyfish>());

            Vector2 laserDirection8 = MathTool.RotateVector2(laserDirection, Mathf.Deg2Rad * 315);
            Vector2 laserStartPosition8 = originalPos + laserDirection8 * instantiateDistanceOffset;
            GameObject jellyfish8 = Object.Instantiate(jellyfishPrefab, laserStartPosition8, Quaternion.identity);
            jellyfish8.GetComponent<Jellyfish>().Init(stateMachine);
            jellyfish8.GetComponent<Jellyfish>().LaserDirection = laserDirection8;
            jellyfish8.GetComponent<Jellyfish>().rotateAngle = rotateAngle;
            jellyfish8.GetComponent<Jellyfish>().rotateTime = rotateTime;
            stateMachine.jellyfishes.Add(jellyfish8.GetComponent<Jellyfish>());
        }
    }
}