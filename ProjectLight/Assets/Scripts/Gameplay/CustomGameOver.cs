using System.Collections.Generic;
using UnityEngine;

public class CustomGameOver : MonoBehaviour
{
    [SerializeField
#if UNITY_EDITOR
        ,ReadOnly
#endif
        ] private List<ServantSpiderStateMachine> m_servant_spiders = new List<ServantSpiderStateMachine>();
    [SerializeField] private UILogic m_ui_logic;

    void Start()
    {
        m_servant_spiders.AddRange(FindObjectsOfType<ServantSpiderStateMachine>());
    }

    public void OnSpiderDeath(ServantSpiderStateMachine spider)
    {
        if (m_servant_spiders.Contains(spider))
        {
            m_servant_spiders.Remove(spider);
        }

        if (m_servant_spiders.Count == 0)
        {
            m_ui_logic.GameOver(true);
        }
    }

    private static CustomGameOver s_instance;

    public static CustomGameOver GetInstance() // => s_instance;
    {
        if (s_instance == null)
        {
            return null;
        }

        return s_instance;
    }

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            s_instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

}
