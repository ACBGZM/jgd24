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
                rowLaser.Init(stateMachine);
                skillDuration = rowLaser.duration;
                animator.Play("Octopus_RowLaser");
                AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { animator.Play("Octopus_Idle"); });
            }
            if (skill.GetType() == typeof(Octopus_FiveLaser))
            {
                Debug.Log("FiveLaser");
                Octopus_FiveLaser fiveLaser = (Octopus_FiveLaser)skill;
                skillDuration = fiveLaser.duration;
                fiveLaser.Init(stateMachine);
                if(stateMachine.Phase2)
                {
                    animator.Play("Octopus_FiveShot_Phase2");
                }
                else 
                {
                    animator.Play("Octopus_FiveShot");
                }
                AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { animator.Play("Octopus_Idle"); });
            }
            if (skill.GetType() == typeof(Octopus_RotateLaser))
            {
                Debug.Log("RotateLaser");
                Octopus_RotateLaser rotateLaser = (Octopus_RotateLaser)skill;
                rotateLaser.Init(stateMachine);
                skillDuration = rotateLaser.duration;
                animator.Play("Octopus_RotateLaser");
                AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { animator.Play("Octopus_Idle"); });
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

    public override void Exit()
    {
        base.Exit();
        foreach (Jellyfish jellyfish in stateMachine.jellyfishes)
        {
            if(jellyfish != null)
            {
                jellyfish.GetComponent<Jellyfish>().Dead();
            }
        }
        stateMachine.jellyfishes.Clear();
    }
}