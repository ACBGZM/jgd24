using Cinemachine;
using System.Security.Cryptography;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    private CinemachineConfiner2D m_confiner_2d;
    [SerializeField] private CinemachineImpulseSource m_impulse_source;

    private void Awake()
    {
        m_confiner_2d = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        GameplayEventManager.StartListening("OnDetonateAllBombs", GenerateImpulse);
    }

    private void OnDisable()
    {
        GameplayEventManager.StopListening("OnDetonateAllBombs", GenerateImpulse);
    }

    private void Start()
    {
        GetSceneCinemachineConfiner();
    }

    private void GetSceneCinemachineConfiner()
    {
        var obj = GameObject.FindGameObjectWithTag("CinemachineConfiner");
        if (obj == null)
        {
            return;
        }

        m_confiner_2d.InvalidateCache();
        m_confiner_2d.m_BoundingShape2D = obj.GetComponent<Collider2D>();
    }

    void GenerateImpulse(object obj)
    {
        m_impulse_source.GenerateImpulse();
    }
}
