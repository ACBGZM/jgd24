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
                Debug.Log("Death");
                // TODO: 广播Death事件
                m_ui_logic.GameOver(false);
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
    private float laserDamageProtectTime = 0.1f;
    private float currentLaserTime;

    [SerializeField] private UILogic m_ui_logic;

    void Start()
    {   
        currentLaserTime = 0;

        Health = maxHealth;
        HealthChangeEvent.CallOnHealthChanged(Health,gameObject);
      
    }

    void FixUpdate()
    {
        currentLaserTime -= Time.deltaTime;
    }

    public void OnBombHit(int damage)
    {   
        bool monsterCanBeDamaged = true;
        if (gameObject.CompareTag("Boss"))
        {
            monsterCanBeDamaged = GetComponent<SpiderStateMachine>().canBeDamaged;
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
            monsterCanBeDamaged = GetComponent<SpiderStateMachine>().canBeDamaged;
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
            ServantHealthBar healthBar =  GetComponentInChildren<ServantHealthBar>();
            healthBar.Change(Health,maxHealth);
        }

    }
}
