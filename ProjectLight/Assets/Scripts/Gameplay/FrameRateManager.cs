
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 300;
    }
}
