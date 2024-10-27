using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour,BombDamage
{
    

    [Header("外观")]
    [SerializeField]
    public Sprite lightSprite;
    [SerializeField]
    public Sprite darkSprite;
    private SpriteRenderer m_sprite;

    private Light2D m_candleLight;


     [Header("状态")]
    public bool lightStatus = false;

    [Header("点亮时长")]
    [SerializeField]
    private float lightTime = 5.0f;
    [SerializeField]
    private float restlightTime;
   


    public  void Start()
    {   
        m_sprite = GetComponent<SpriteRenderer>();
        m_candleLight = GetLightObj();
        restlightTime = lightStatus?lightTime:0;
        SwitchLight(lightStatus);
    }

    public void FixedUpdate()
    {
        if(lightStatus)
        {
            restlightTime -= Time.deltaTime;
             if (restlightTime <= 0)
            {
                SwitchLight(false);
            }
        }
    }

    public void OnBombHit(int damage)
    {
        restlightTime = lightTime;
        SwitchLight(true);
    }


    private void SwitchLight(bool status)
    {   
        if(status !=  lightStatus)
        {
            WwiseAudioManager.GetInstance().PostEvent("candel_switch_light", gameObject);
        }

        lightStatus = status;
        if(m_candleLight != null)
        {
            m_candleLight.enabled = status;
        }
        m_sprite.sprite = status ? lightSprite : darkSprite;
    }


    private Light2D GetLightObj()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            Light2D light2D = child.GetComponent<Light2D>();
            if (light2D!= null)
            {   
                return light2D;
            }
        }
        return null;
    }

}
