using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{   
    //
    public GameObject player;
    public GameObject boss;
    // public GameObject servant;

    // 血条
    [Header("血条")]
    public Slider playerHealthSlider;
    public Slider bossHealthSlider;

    //
    [Header("全局光照")]
    public Light2D globalLight;
    public float lightIntensity=0.02f;
    public float stageTwoHealthThreshold = 0.4f;
   
    private void OnEnable()
    {   
        playerHealthSlider.maxValue = player.GetComponent<PlayerOnHit>().maxHealth;
        bossHealthSlider.maxValue = boss.GetComponent<BossOnHit>().maxHealth;
        HealthChangeEvent.OnHealthChanged += ResponseHealthChanged;
    }

    private void OnDisable()
    {
        HealthChangeEvent.OnHealthChanged -= ResponseHealthChanged;
    }

    public void ResponseHealthChanged(int newHealth, GameObject sourceObject)
    {
        if (sourceObject == player)
        {
            playerHealthSlider.value = newHealth;

        }
        else if (sourceObject == boss)
        {
            bossHealthSlider.value = newHealth; //UpdateHealthBar
            int bossMaxHealth = boss.GetComponent<BossOnHit>().maxHealth;
            float curHealthRate = (float)newHealth/(float)bossMaxHealth;


            if(curHealthRate <= stageTwoHealthThreshold)
            {   
                globalLight.intensity = lightIntensity;
                // 其他灯光设置
            } 
        }
        //  else if (sourceObject == servant)
        // {
        //     // 小蜘蛛扣血处理 -> 血条
        //     //  newHealth

        // }
    }


}
