using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Skill", fileName = "SpiderState_Skill")]
public class SpiderState_Skill : SpiderState
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
        stateMachine.SetCurrentStateType(SpiderStateType.Skill);
        stateMachine.UpdateServantAmount();
        skill = skillList[Random.Range(0, skillList.Count)];

        if (skill != null)
        {
            if (skill.GetType() == typeof(BulletSkill))
            {
                BulletSkill bulletSkill = (BulletSkill)skill;
                bulletSkill.Init(stateMachine.transform.position, stateMachine.GetPlayerPosition());
                /* stateMachine.animationPlayControl.PlayAnimation(bulletSkill.animationClipName, null, () =>
                {
                    stateMachine.GoToNextState();
                });
                */
                animator.Play(bulletSkill.animationClipName);
                AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { stateMachine.GoToNextState(); });
                // bulletSkill.Cast();
                /*
                foreach (BulletLauncherStat launcherStat in bulletSkill.launcherStats)
                {
                    Quaternion bulletRotation = bulletSkill.aimToPlayer
                        ? Quaternion.LookRotation(Vector3.forward,
                            stateMachine.GetPlayerPosition() - (Vector2)stateMachine.transform.position)
                        : Quaternion.identity;
                    GameObject launcher = Instantiate(bulletSkill.launcherPrefab, stateMachine.transform.position,
                        bulletRotation);
                    launcher.GetComponent<BulletLauncher>().Init(launcherStat);
                }
                */
            }
            if (skill.GetType() == typeof(SummonSkill))
            {
                SummonSkill summonSkill = (SummonSkill)skill;
                int summonAmount = Mathf.Clamp(stateMachine.maxServantAmount - stateMachine.currentServantAmonut, 0, summonSkill.SummonAmount);
                for(int i = 0; i < summonAmount; i ++)
                {
                    float targetAngle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                    Vector2 targetVector = new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle));
                    float targetDistance = UnityEngine.Random.Range(summonSkill.MinSummonRadius, summonSkill.MaxSummonRadius);
                    Vector2 summonPosition = (Vector2)stateMachine.transform.position + targetVector * targetDistance;
                    Instantiate(summonSkill.summonPrefab, summonPosition, Quaternion.identity);
                    stateMachine.currentServantAmonut ++;
                }
                stateMachine.GoToNextState();
            }
        }
    }

    public override void Execute()
    {
        base.Execute();
    }
}