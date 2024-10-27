using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{   
    //
    public GameObject player;
    public GameObject boss;

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
            // playerHealthSlider.maxValue = player.GetComponent<PlayerOnHit>().maxHealth;
            // playerHealthSlider.value = player.GetComponent<PlayerOnHit>().Health;
            playerHealthSlider.value = newHealth;

        }
        else if (sourceObject == boss)
        {
            // bossHealthSlider.maxValue = boss.GetComponent<BossOnHit>().maxHealth;
            // bossHealthSlider.value = boss.GetComponent<BossOnHit>().Health;
            bossHealthSlider.value = newHealth; //UpdateHealthBar

            float curHealthRate = Mathf.Clamp(newHealth / boss.GetComponent<BossOnHit>().Health, 0.1f, 1.0f);
            if(curHealthRate <= stageTwoHealthThreshold)
            {
                globalLight.intensity = lightIntensity;
                // 其他灯光设置
            } 


        }
    }

    // public void UpdateHealthBar()
    // {
         // 
    // boss.GetComponent<BossOnHit>().Health;
    // }

    // public void SwitchGlobalLight()
    // {

    // }


}
