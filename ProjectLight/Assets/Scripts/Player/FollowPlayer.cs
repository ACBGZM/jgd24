using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followRatio = 0.2f;

    [SerializeField] private bool m_enable_x = true;
    [SerializeField] private bool m_enable_y = true;

    private Vector2 pos;
    private Vector2 player_position_cache;
    private float offset_x;
    private float offset_y;

    private void Start()
    {
        if (player != null)
        {
            player_position_cache = player.transform.localPosition;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            pos = transform.localPosition;

            if (m_enable_x)
            {
                offset_x = (player.transform.localPosition.x - player_position_cache.x) * followRatio;
                pos.x += offset_x;
            }

            if (m_enable_y)
            {
                offset_y = (player.transform.localPosition.y - player_position_cache.y) * followRatio;
                pos.y += offset_y;
            }

            transform.localPosition = pos;
            player_position_cache = player.transform.localPosition;
        }
    }
}