using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Laser Skill", menuName = "Skill/Laser Skill")]
public class LaserSkill : Skill
{
    public GameObject jellyfishPrefab;
    public bool aimToPlayer = false;
    public Vector2 originalPos;
    public Vector2 playerPos;

    public float duration;

    public void Init(Vector2 originalPos, Vector2 playerPos)
    {
        this.originalPos = originalPos;
        this.playerPos = playerPos;
    }

    public void UpdateOriginalPos(Vector2 originalPos)
    {
        this.originalPos = originalPos;
    }
    public void UpdatePlayerPos(Vector2 playerPos)
    {
        this.playerPos = playerPos;
    }

    public void Cast()
    {
        
    }
}