using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, BombDamage
{

    // Box是否可重生
        [Header("重生设置")]
     [SerializeField] private bool renewable = false; 
     [SerializeField] private float respawnDelayTime = 5.0f; 

    // 掉落物Prefab
    // public GameObject dropItemPrefab;

    // 破坏动画
    public GameObject boxExplodeAnimPrefab;

    // Box血量
    private int health = 1; 
    public int Health
    {   
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health<=0)
            {
                // Box毁坏
                BoxDestored();
            }
            else
            {
                // Box受损 (如设计Box需多次爆破时实现)
                Debug.Log("Box受损"); 
            }
        }
    }

    private Collider2D m_collider;
    private SpriteRenderer m_sprite;
    public List<Sprite> spriteList;

    public virtual void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();

    }


    // 伤害挂载
    // BombDamage接口
    public void OnBombHit(int damage)
    {
        Health -= 1; // 具体伤害由damage折算
    }



    // Box毁坏
    public void BoxDestored()
    {   
        Debug.Log("Box毁坏");

        WwiseAudioManager.GetInstance().PostEvent("box_break", gameObject);

        // Box被完全破坏
        if(renewable)
        {   
            // Debug.Log("renewable");
            // 被破坏动画
            GameObject newBoxExplodeAim =  Instantiate(boxExplodeAnimPrefab, transform.position, Quaternion.identity);

            // 可重生Box
            m_sprite.sprite = spriteList[1]; // 更改贴图
            m_collider.enabled = false; // 暂停碰撞检测
            // m_collider.isTrigger = true;
            
            CreateDropItem();
            Invoke("BoxRespawn", respawnDelayTime); // 重生倒计时
       }
       else
        {   
            // Debug.Log("not renewable");
            GameObject newBoxExplodeAim =  Instantiate(boxExplodeAnimPrefab, transform.position, Quaternion.identity);
            CreateDropItem();
            Destroy(gameObject);
        }
    }

    // 生成掉落物
    public void CreateDropItem()
    {
        // 从Controller加载掉落物概率表? {boxID:{ item1: 0.8, item2:0.1, none:0.1 } } 
        // 根据概率表计算DropItem ID
        
        // float randNum = Random.Range(0f, 1f);
        // int ItemID = 1;
        
        // GameObject newDropItem =  Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        // DropItem newDropItemScript = newDropItem.GetComponent<DropItem>();
        // newDropItemScript.InitItem(ItemID);
        DropArea createScript = GetComponent<DropArea>();
        createScript.OnCreateDropItem();
        
        // if(renewable)
        // {
        //     newDropItem.transform.SetParent(transform);
        // }
    }


    public virtual void BoxRespawn()
    {   
        //碰撞检测
        bool respawnStatus = GetComponentInChildren<BoxRespawnDetection>().canRespawn;
        if(respawnStatus)
        {
            BoxInit();
        }
        else
        {
            Invoke("BoxRespawn", 0.5f);
        }
        
    }
    
    public void BoxInit()
    {
         // 销毁全部子物体
        // Transform[] children = GetComponentsInChildren<Transform>();
        // foreach (Transform child in children)
        // {
        //     if (child!= transform)
        //     {
        //         Destroy(child.gameObject);
        //     }
        // }

        m_sprite.sprite = spriteList[0]; // 更改贴图
        m_collider.enabled = true; // 开启碰撞检测
        //  m_collider.isTrigger = false;
    }


}
