using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOnHit : MonoBehaviour, BombDamage, LaserDamage
{   
     [SerializeField]
     public int maxHealth = 1000;
     [SerializeField]
    private int health = 1000;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Dead();
                // TODO: 广播Death事件
            }
            else
            {
                Debug.Log("OnHit");
            }
        }
    }

     [SerializeField]
    public float bombDamageRate = 1.0f;
    public float shieldBombDamageRate = 0.1f;

    [SerializeField]
    public float laserDamageRate = 1.0f;
    public float shieldLaserDamageRate = 0.1f;

    [SerializeField]
    private float bombDamageProtectTime = 0.5f;
    private float currentBombTime;

    [SerializeField]
    private float laserDamageProtectTime = 0.1f;
    private float currentLaserTime;

    [SerializeField] private UILogic m_ui_logic;

    void Start()
    {   
        currentLaserTime = 0;
        currentBombTime = 0;

        Health = maxHealth;
        HealthChangeEvent.CallOnHealthChanged(Health,gameObject);
      
    }

    void FixUpdate()
    {
        currentLaserTime -= Time.deltaTime;
        currentBombTime -= Time.deltaTime;
    }

    public void OnBombHit(int damage)
    { 

        if (currentBombTime > 0)
        {
            return;
        }

        bool monsterCanBeDamaged = true;
        if (gameObject.CompareTag("Boss"))
        {
            if(GetComponent<SpiderStateMachine>() != null)
            {
                monsterCanBeDamaged = GetComponent<SpiderStateMachine>().canBeDamaged;
            }
            else if(GetComponent<OctopusStateMachine>() != null)
            {
                monsterCanBeDamaged = GetComponent<OctopusStateMachine>().canBeDamaged;
            }
        }
        else
        {
            //  monsterCanBeDamaged = GetComponent<ServantSpiderStateMachine>().canBeDamaged;
        }

        if(monsterCanBeDamaged)
        {
            Health -= (int)(damage * bombDamageRate);
        }
        else{
            Health -= (int)(damage * shieldBombDamageRate);
        }

        currentBombTime = bombDamageProtectTime;

        OnHit();
    }

    public void OnLaserHit(int damage)
    {
        if (currentLaserTime > 0)
        {
            return;
        }



        bool monsterCanBeDamaged = true;
        if (gameObject.CompareTag("Boss"))
        {
            if(GetComponent<SpiderStateMachine>() != null)
            {
                monsterCanBeDamaged = GetComponent<SpiderStateMachine>().canBeDamaged;
            }
            else if(GetComponent<OctopusStateMachine>() != null)
            {
                Debug.Log("八爪鱼受到伤害");
                monsterCanBeDamaged = GetComponent<OctopusStateMachine>().canBeDamaged;
            }
        }
        else
        {
             monsterCanBeDamaged = GetComponent<ServantSpiderStateMachine>().canBeDamaged;
        }
        
        if(monsterCanBeDamaged)
        {
             Health -= (int)(damage * laserDamageRate);
        }
        else{
            Health -= (int)(damage * shieldLaserDamageRate);
        }

        currentLaserTime = laserDamageProtectTime;
        OnHit();
      
    }

    public void OnHit()
    {   

        Debug.Log("TODO: 怪物受击动画");
        if (gameObject.CompareTag("Boss"))
        {
        HealthChangeEvent.CallOnHealthChanged(Health,gameObject);
        }
        if (gameObject.CompareTag("Servant"))
        {   
            Debug.Log("小蜘蛛受击");
            Debug.Log(Health);
            ServantHealthBar healthBar = GetComponentInChildren<ServantHealthBar>();
            healthBar.Change(Health,maxHealth);
        }
    }

    public void Dead()
    {
        if(GetComponent<SpiderStateMachine>() != null)
        {
            GetComponent<SpiderStateMachine>().Dead();
        }
        else if (GetComponentInParent<ServantSpiderStateMachine>() != null)
        {
            GetComponentInParent<ServantSpiderStateMachine>().Dead();
        }
        else if (GetComponent<OctopusStateMachine>() != null)
        {
            GetComponent<OctopusStateMachine>().Dead();
        }
        Debug.Log("Dead");
    }

    public void GameOver(bool win)
    {
        m_ui_logic.GameOver(win);
    }
}
