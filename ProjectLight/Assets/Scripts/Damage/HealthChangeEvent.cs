using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChangeEvent
{
    public static event Action<int, GameObject> OnHealthChanged;

    public static void CallOnHealthChanged(int newHealth, GameObject sourceObject)
    {   
        OnHealthChanged?.Invoke(newHealth, sourceObject);
    }
}



