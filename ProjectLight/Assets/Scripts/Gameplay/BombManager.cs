using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    private bool m_is_detonating = false;

    private static BombManager s_instance;

    [SerializeField]
    [Tooltip("放置偏移")]
    public Vector2 plantOffet;

    private BombManager() { }
    public static BombManager GetInstance() // => s_instance;
    {
        if (s_instance == null)
        {
            lock (typeof(BombManager))
            {
                if (s_instance == null)
                {
                    GameObject go = new GameObject("BombManager");
                    s_instance = go.AddComponent<BombManager>();
                    DontDestroyOnLoad(go);
                }
            }
        }

        return s_instance;
    }

    [SerializeField] private GameObject m_bomb_prefab;
    private List<Bomb> m_bombs = new List<Bomb>();

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlantABomb(Transform transform)
    {
        if (m_bomb_prefab != null)
        {
            GameObject bomb = GameObject.Instantiate(m_bomb_prefab, transform.position + new Vector3(plantOffet.x, plantOffet.y, 0), transform.rotation);
            bomb.transform.SetParent(null);
            m_bombs.Add(bomb.GetComponent<Bomb>());
        }
    }

    public void DetonateAllBombs()
    {
        if (m_bombs.Count > 0 && !m_is_detonating)
        {
            StartCoroutine(Explode());
            GameplayEventManager.TriggerEvent("OnDetonateAllBombs");
        }
    }

    private IEnumerator Explode()
    {
        m_is_detonating = true;

        foreach (Bomb bomb in m_bombs)
        {
            bomb.Explode();
        }

        yield return YieldHelper.WaitForSeconds(1.0f);

        foreach (Bomb bomb in m_bombs)
        {
            Destroy(bomb.gameObject);   // todo
        }

        m_bombs.Clear();

        m_is_detonating = false;
    }
}