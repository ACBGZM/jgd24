using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WwiseEvent
{
    public string m_name;
    public AK.Wwise.Event m_event;
}

public class WwiseAudioManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.State m_state;

    [SerializeField] private List<WwiseEvent> m_level_audio_bank;
    [SerializeField] private Dictionary<string, WwiseEvent> m_level_audio_ban2k;

    private static WwiseAudioManager s_instance;

    public static WwiseAudioManager GetInstance() // => s_instance;
    {
        if (s_instance == null)
        {
            lock (typeof(WwiseAudioManager))
            {
                if (s_instance == null)
                {
                    GameObject go = new GameObject("WwiseAudioManager");
                    s_instance = go.AddComponent<WwiseAudioManager>();
                    //DontDestroyOnLoad(go);
                }
            }
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

        AkSoundEngine.SetState(m_state.GroupId, m_state.Id);
    }

    public void PostEvent(string name, GameObject source_go = null)
    {
        WwiseEvent target_event = m_level_audio_bank.Find(t => t.m_name == name);

        if(target_event != null)
        {
            target_event.m_event.Post(source_go ? source_go : gameObject);
        }
        else
        {
            Debug.LogError("cannot find wwise event with name " + name);
        }
    }

    public void StopEvent(string name, GameObject source_go = null)
    {
        WwiseEvent target_event = m_level_audio_bank.Find(t => t.m_name == name);

        if (target_event != null)
        {
            target_event.m_event.Stop(source_go ? source_go : gameObject);
        }
        else
        {
            Debug.LogError("cannot find wwise event with name " + name);
        }
    }

    private void Start()
    {
        PostEvent("bgm");
    }
}
