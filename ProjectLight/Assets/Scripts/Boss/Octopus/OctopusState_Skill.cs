using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OctopusState_Skill", menuName = "StateMachine/OctopusState/Skill")]
public class OctopusState_Skill : OctopusState
{
    public List<Skill> skillList;

    [SerializeField
#if UNITY_EDITOR
    , ReadOnly
#endif
    ]
    private Skill skill;

    private float skillDuration;
    private float timer = 0;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        skill = skillList[Random.Range(0, skillList.Count)];

        if (skill != null)
        {
            if (skill.GetType() == typeof(LaserSkill))
            {
                Debug.Log("LaserSkill");
                LaserSkill laserSkill = (LaserSkill)skill;
                skillDuration = laserSkill.duration;
                stateMachine.LaserCast(laserSkill);
            }
            if (skill.GetType() == typeof(Octopus_RowLaser))
            {
                Debug.Log("RowLaser");
                Octopus_RowLaser rowLaser = (Octopus_RowLaser)skill;
                skillDuration = rowLaser.duration;
                stateMachine.LaserCast(rowLaser);
            }
            if (skill.GetType() == typeof(Octopus_FiveLaser_Phase2))
            {
                Debug.Log("FiveLaser");
                Octopus_FiveLaser_Phase2 fiveLaser = (Octopus_FiveLaser_Phase2)skill;
                skillDuration = fiveLaser.duration;
                stateMachine.LaserCast(fiveLaser);
            }
        }
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= skillDuration)
        {
            stateMachine.GoToNextState();
        }
    }

}