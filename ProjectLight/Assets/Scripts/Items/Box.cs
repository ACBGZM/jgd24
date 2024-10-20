using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, BombDamage
{

    // Box是否可重生
    public bool renewable = false; 

    // 掉落物Prefab
    public GameObject dropItemPrefab;

    // 破坏动画
    public GameObject boxExplodePrefab;

    // Box血量
    private int Health = 1; 
    public int health
    {   
        get
        {
            return Health;
        }
        set
        {
            Health = value;
            if(Health<=0)
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

    public void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();

    }

    // BombDamage接口
    public void OnHit(int damage)
    {
        health -= 1; // 具体伤害由damage折算
    }

    // Box毁坏
    public void BoxDestored()
    {   
        Debug.Log("Box毁坏");
        

        // Box被完全破坏
        if(renewable)
        {   
            // 被破坏动画
            GameObject newBoxExplode =  Instantiate(boxExplodePrefab, transform.position, Quaternion.identity);

            // 可重生Box
            m_sprite.sprite = spriteList[1]; // 更改贴图
            m_collider.enabled = false; // 暂停碰撞检测
            
            CreateDropItem();
            Invoke("BoxRespawn", 5f); // 重生倒计时
       }
       else
        {   
            GameObject newBoxExplode =  Instantiate(boxExplodePrefab, transform.position, Quaternion.identity);
            CreateDropItem();
            Destroy(gameObject);
        }
    }

    // 生成掉落物
    public void CreateDropItem()
    {
        // 从Controller加载掉落物概率表? {boxID:{ item1: 0.8, item2:0.1, none:0.1 } } 
        // 根据概率表计算DropItem ID
        
        float randNum = Random.Range(0f, 1f);
        int ItemID = 1;
        
        GameObject newDropItem =  Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        DropItem newDropItemScript = newDropItem.GetComponent<DropItem>();
        newDropItemScript.InitItem(ItemID);
        
        // todo: 如果需要销毁当前格中道具，将新生成的道具父物体设置为当前box？ 在线等一个策划案
        if(renewable)
        {
            newDropItem.transform.SetParent(transform);
        }
    }


    public void BoxRespawn()
    {   
        // 销毁全部子物体
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child!= transform)
            {
                Destroy(child.gameObject);
            }
        }

        m_sprite.sprite = spriteList[0]; // 更改贴图
        m_collider.enabled = true; // 开启碰撞检测
    }
        
}
