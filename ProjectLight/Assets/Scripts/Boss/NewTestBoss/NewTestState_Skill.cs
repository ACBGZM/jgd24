using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/NewTestBossState/Skill", fileName = "NewTestState_Skill")]
public class NewTestState_Skill : NewTestState
{
    public List<Skill> skillList;
    private Skill skill;

    public override void Enter()
    {
        base.Enter();

        // 随机选择一个技能
        skill = skillList[Random.Range(0, skillList.Count)];
    }

    public override void Execute()
    {
        base.Execute();

        if (skill != null)
        {
            if (skill.GetType() == typeof(BulletSkill))
            {
                BulletSkill bulletSkill = (BulletSkill) skill;
                foreach (BulletLauncherStat launcherStat in bulletSkill.launcherStats)
                {
                    Quaternion bulletRotation = bulletSkill.aimToPlayer 
                    ? Quaternion.LookRotation(Vector3.forward, stateMachine.GetPlayerPosition() - (Vector2)stateMachine.transform.position)
                    : Quaternion.identity;
                    GameObject launcher = Instantiate(bulletSkill.launcherPrefab, stateMachine.transform.position, bulletRotation);
                    launcher.GetComponent<BulletLauncher>().Init(launcherStat);
                }
            }
        }
        stateMachine.GoToNextState();
    }
}