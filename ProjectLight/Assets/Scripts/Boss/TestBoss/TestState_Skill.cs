using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/TestBossState/Skill", fileName = "TestState_Skill")]
public class TestState_Skill : TestState
{
    Skill skill;

    public override void Enter()
    {
        base.Enter();
        skill = stateMachine.skills[stateMachine.nextSkillIndex];
    }

    public override void Execute()
    {
        base.Execute();
        if (skill != null)
        {
            if (skill.GetType() == typeof(BulletSkill))
            {
                BulletSkill bulletSkill = (BulletSkill) skill;
                foreach(BulletLauncherStat launcherStat in bulletSkill.launcherStats)
                {
                    GameObject launcher = Instantiate(bulletSkill.launcherPrefab, stateMachine.transform.position, Quaternion.identity);
                    launcher.GetComponent<BulletLauncher>().Init(launcherStat);
                }
            }
        }
        stateMachine.ChangeState(typeof(TestState_Idle));
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.nextSkillIndex++;
        if (stateMachine.nextSkillIndex >= stateMachine.skills.Count)
        {
            stateMachine.nextSkillIndex = 0;
        }
    }

}