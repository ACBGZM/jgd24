using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MirrorBrokenEvent
{
    public static event Action ReportMirrorBroken;
   

    public static void CallReportMirrorBroken()
    {   
        ReportMirrorBroken?.Invoke();
    }



}
