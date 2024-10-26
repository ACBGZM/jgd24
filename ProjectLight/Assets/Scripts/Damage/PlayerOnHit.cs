using System;
using UnityEngine;

public class PlayerOnHit : MonoBehaviour, BombDamage, LaserDamage, BossDamage, BulletDamage
{   

    [SerializeField]
     public int maxHealth = 100;
    [SerializeField]
    private int health = 100;
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
            }
            else
            {
                Debug.Log("OnHit");
            }
        }
    }

    [SerializeField]
    public float bombDamageRate = 1.0f;

    [SerializeField]
    public float laserDamageRate = 1.0f;

    [SerializeField]
    public float bossDamageRate = 1.0f;

    [SerializeField]
    public float bulletDamageRate = 1.0f;

    [SerializeField]
    private float bombDamageProtectTime = 0.5f;
    private float currentBombTime;

    [SerializeField]
    private float laserDamageProtectTime = 0.5f;
    private float currentLaserTime;

    [SerializeField]
    private float bossDamageProtectTime = 0.5f;
    private float currentBossTime;

    [SerializeField]
    private float bulletDamageProtectTime = 0.5f;
    private float currentBulletTime;

    private Animator m_animator = null;
    private Action m_on_hit_action = null;


    void Awake()
    {
        currentBombTime = 0;
        currentLaserTime = 0;
        currentBossTime = 0;
        currentBulletTime = 0;
    }

    private void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
       
    }

    public void FixedUpdate()
    {
        currentBombTime -= Time.deltaTime;
        currentLaserTime -= Time.deltaTime;
        currentBossTime -= Time.deltaTime;
        currentBulletTime -= Time.deltaTime;
    }

    public void OnBombHit(int damage)
    {
        if (currentBombTime <= 0)
        {
            Health -= (int)(damage * bombDamageRate);
            currentBombTime = bombDamageProtectTime;
            OnHit();
        }
    }

    public void OnLaserHit(int damage)
    {
        if (currentLaserTime <= 0)
        {
            Health -= (int)(damage * laserDamageRate);
            currentLaserTime = laserDamageProtectTime;
            OnHit();
        }
    }

    public void OnBossHit(int damage)
    {
        if (currentBombTime <= 0)
        {
            Health -= (int)(damage * bossDamageRate);
            currentBossTime = bossDamageProtectTime;
            OnHit();
        }
    }

    public void OnBulletHit(int damage)
    {
        if (currentBulletTime <= 0)
        {
            Health -= (int)(damage * bulletDamageRate);
            currentBulletTime = bulletDamageProtectTime;
            OnHit();
        }
    }

    public void SetOnHitAction(Action action)
    {
        m_on_hit_action = action;
    }

    public void OnHit()
    {   
        HealthChangeEvent.CallOnHealthChanged(Health,gameObject);

        m_animator.SetTrigger("Hit");
        m_on_hit_action?.Invoke();

        WwiseAudioManager.GetInstance().PostEvent("player_on_hit", gameObject);
    }
}
