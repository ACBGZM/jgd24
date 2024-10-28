using System;
using System.Collections.Generic;

public static class GameplayEventManager
{
    private static Dictionary<string, Action<object>> m_event_dictionary = new Dictionary<string, Action<object>>();

    public static void StartListening(string event_name, Action<object> listener)
    {
        if (m_event_dictionary.TryGetValue(event_name, out Action<object> target_event))
        {
            target_event += listener;
            m_event_dictionary[event_name] = target_event;
        }
        else
        {
            target_event += listener;
            m_event_dictionary.Add(event_name, target_event);
        }
    }

    public static void StopListening(string m_event_name, Action<object> listener)
    {
        if (m_event_dictionary.TryGetValue(m_event_name, out Action<object> target_event))
        {
            target_event -= listener;
            if (target_event == null)
            {
                m_event_dictionary.Remove(m_event_name);
            }
            else
            {
                m_event_dictionary[m_event_name] = target_event;
            }
        }
    }

    public static void TriggerEvent(string event_name, object even_param = null)
    {
        if (m_event_dictionary.TryGetValue(event_name, out Action<object> target_event))
        {
            target_event?.Invoke(even_param);
        }
    }
}