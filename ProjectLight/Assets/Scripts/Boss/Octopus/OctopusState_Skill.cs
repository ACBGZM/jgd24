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
                foreach (LaserLauncherStat launcherStat in laserSkill.launcherStats)
                {
                    GameObject launcher = Instantiate(laserSkill.launcherPrefab, stateMachine.transform.position, Quaternion.identity);
                    launcher.GetComponent<LaserLauncher>().Init(launcherStat);
                }
            }
        }
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            stateMachine.GoToNextState();
        }
    }

}