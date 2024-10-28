using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;
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
    
    public float targetIntensity=0.02f;
    // private float curIntensity=1.0f;
    public float lightDimmingTime = 0.2f;
    private Coroutine _adjustLightIntensityCoroutine;
    
    public float stageTwoHealthThreshold = 0.4f;

    private bool isBossSecondStage = false;



   
    private void OnEnable()
    {   
        playerHealthSlider.maxValue = player.GetComponent<PlayerOnHit>().maxHealth;
        playerHealthSlider.value = player.GetComponent<PlayerOnHit>().maxHealth;

        bossHealthSlider.maxValue = boss.GetComponent<BossOnHit>().maxHealth;
        bossHealthSlider.value = boss.GetComponent<BossOnHit>().maxHealth;

        if(boss.GetComponent<SpiderStateMachine>() != null)
        {
            boss.GetComponent<SpiderStateMachine>().healthManager = this;
        }

        HealthChangeEvent.OnHealthChanged += ResponseHealthChanged;
    }
    

    private void OnDisable()
    {
        HealthChangeEvent.OnHealthChanged -= ResponseHealthChanged;
    }


    void Start()
    {
        // curIntensity = globalLight.intensity;
    }


    private IEnumerator AdjustLightIntensity()
    {
        while(globalLight.intensity - targetIntensity > 0.001f)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity,targetIntensity,Time.deltaTime * (1/(lightDimmingTime+0.00001f)));
            //  (1/(lightDimmingTime+0.0001f)))
            Debug.Log("光强"+globalLight.intensity);
            yield return null;
        }
        globalLight.intensity =  targetIntensity ;
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


            if(curHealthRate <= stageTwoHealthThreshold && !isBossSecondStage)
            {        
                isBossSecondStage = true;
                if(boss.GetComponent<SpiderStateMachine>() != null)
                {
                    // SwitchLight(false);
                    boss.GetComponent<SpiderStateMachine>().ChangePhase();
                }
                else if(boss.GetComponent<OctopusStateMachine>() != null)
                {
                    boss.GetComponent<OctopusStateMachine>().ChangePhase();
                }
            } 
        }
    }

    public void SwitchLight(bool lightON)
    {
        //  if(globalLight != null)
        //     {   
        //         GlobalLightDimming _lightController = globalLight.GetComponent<GlobalLightDimming>();
        //         _lightController.OnLightDimming();
        //     }
        //     // globalLight.intensity = lightIntensity;
        //     // 其他灯光设置

 
    
        if(_adjustLightIntensityCoroutine != null)
        {
            StopCoroutine(_adjustLightIntensityCoroutine);
        }

        _adjustLightIntensityCoroutine = StartCoroutine(AdjustLightIntensity());
    }
}
