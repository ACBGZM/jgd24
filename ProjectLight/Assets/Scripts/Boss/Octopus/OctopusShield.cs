using UnityEngine;

public class OctopusShield : MonoBehaviour
{
    public float shieldLifeTime = 0.5f;
    public float shieldLifeTimer;
    private OctopusStateMachine stateMachine;

    void Awake()
    {
        stateMachine = GetComponent<OctopusStateMachine>();
    }

    public void OnLaserHit()
    {
        shieldLifeTimer += Time.fixedDeltaTime;
        if (shieldLifeTimer >= shieldLifeTime)
        {
            if(stateMachine != null)
            {
                stateMachine.Palsy();
                shieldLifeTimer = 0;
            }
        }
    }
}