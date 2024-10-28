using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Skill/SummonSkill", fileName = "new SummonSkill")]
public class SummonSkill : Skill
{
    public GameObject summonPrefab;
    [SerializeField]
    private int summonAmount;
    public int SummonAmount => summonAmount;

    [SerializeField]
    private float minSummonRadius;
    public float MinSummonRadius => minSummonRadius;
    [SerializeField]
    private float maxSummonRadius;
    public float MaxSummonRadius => maxSummonRadius;
}