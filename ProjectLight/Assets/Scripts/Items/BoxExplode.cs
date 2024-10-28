using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxExplode : MonoBehaviour
{
    public void OnAnimationEnd()
   {
       // 销毁物体
       Destroy(gameObject);
   }
}
