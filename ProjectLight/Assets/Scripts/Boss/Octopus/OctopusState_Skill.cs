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
                stateMachine.LaserCast(laserSkill);
            }
            if (skill.GetType() == typeof(Octopus_RowLaser))
            {
                Debug.Log("RowLaser");
                Octopus_RowLaser rowLaser = (Octopus_RowLaser)skill;
                stateMachine.LaserCast(rowLaser);
            }
        }
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            // stateMachine.GoToNextState();
        }
    }

}