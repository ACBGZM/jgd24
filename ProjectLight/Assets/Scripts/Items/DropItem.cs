using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DropItem : MonoBehaviour
{   
    // 掉落物（道具）

    public int itemID = 0; // 道具ID

    // private bool isCollected = false; // 被拾取状态
    // private SpriteRenderer m_sprite;

    // public void InitItem(int id)
    // {
    //     itemID = id;
    //     m_sprite = GetComponent<SpriteRenderer>();
    //     // todo: 更换不同item贴图
    // }

    public void OnTriggerEnter2D(Collider2D other)
    {   

        // if(other.CompareTag("Player") && !isCollected)
        if(other.CompareTag("Player"))
        {   
            // 调用道具拾取函数
            bool collectStatus = Item_PickUp(itemID,other);

            if(collectStatus)
            {
                // isCollected = true;
                Destroy(gameObject);
            }
                
        }
    }

    public bool Item_PickUp(int itemID,Collider2D playerCollider)
    {
        // 依据itemID执行拾取效果
        Debug.Log("拾取道具:"+itemID);

        PlayerController player_controller = playerCollider.gameObject.GetComponent<PlayerController>();
        player_controller.AddItem(itemID);

        // if 达到拾取上限 return false
        return true;
    }


}


