using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServantHealthBar : MonoBehaviour
{

    // public RectTransform _bottomBar;
     public RectTransform _topBar;

     private float _fullWidth;
    //  private float targetWidth => Value * _fullWidth/maxValue;
     void Start()
     {
        _fullWidth = _topBar.rect.width;
     }


     public void Change(int newHealth, int maxHealth)
     {
        float fillRate = Mathf.Clamp((float)newHealth/(float)maxHealth, 0.0f, 1.0f);
        _topBar.sizeDelta = new Vector2(fillRate*_fullWidth, _topBar.rect.height);


        // SetWidth(_topBar, Value);
     }

    //  public void SetWidth(RectTransform rect, float width)
    //  {
    //     rect.sizeDelta = new Vector2(width, rect.rect.height);
    //  }

}
