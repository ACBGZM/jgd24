using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int m_damage = 1;

    [SerializeField] private float     m_explosion_range = 5f;
    [SerializeField] private float     m_explosion_width = 0.5f;
    [SerializeField] private float     m_ray_visible_duration = 1.0f;
    [SerializeField] private int       m_ray_count_per_direction = 3;
    // [SerializeField] private LayerMask m_ray_layer_mask;
    [SerializeField] private string[] m_ray_layer_list;
    [SerializeField] private LayerMask m_ray_layer_mask;

    [SerializeField] private Sprite m_beam_top_sprite;
    [SerializeField] private Sprite m_beam_stretch_sprite;

    private float m_ray_spacing = 0.1f;

    private string m_sorting_layer;
    private int m_order_in_sorting_layer;
    [SerializeField] private Material m_unlit_material;

    public Animator m_animator = null;

    //private LineRenderer[] m_line_renderers;
    private void Awake()
    {
        m_ray_layer_mask = LayerMask.GetMask(m_ray_layer_list);
    }

    protected void Start()
    {
        m_ray_spacing = m_explosion_width / m_ray_count_per_direction;

        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        m_sorting_layer = sprite != null ? sprite.sortingLayerName : "Bomb";
        m_order_in_sorting_layer = sprite != null ? sprite.sortingOrder : 100;

        m_animator = GetComponent<Animator>();
    }

    public void Explode()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            ShootParallelRays(direction);
        }

        //  爆炸动画
        m_animator.SetTrigger("Explode");
        
    }

    private void ShootParallelRays(Vector2 base_direction)
    {
        Vector2 perpendicular_offset = Vector2.Perpendicular(base_direction) * m_ray_spacing;
        Vector2 origin = transform.position;

        HashSet<Collider2D> hitColliders = new HashSet<Collider2D>();

        for (int i = 0; i < m_ray_count_per_direction; i++)
        {
            float offset_multiplier = (i - (m_ray_count_per_direction - 1) / 2f);
            Vector2 ray_origin = origin + offset_multiplier * perpendicular_offset;

            RaycastHit2D[] hits = Physics2D.RaycastAll(ray_origin, base_direction, m_explosion_range, m_ray_layer_mask);

            foreach(RaycastHit2D hit in hits)
            {
                if (hitColliders.Add(hit.collider))
                {
                    BombDamage damageable = hit.collider?.GetComponent<BombDamage>();
                    if (damageable != null)
                    {
                        damageable.OnBombHit(m_damage);
                    }
                }
            }

            // debug draw
            //StartCoroutine(VisualizeRay(ray_origin, base_direction * m_explosion_range, m_ray_visible_duration));

            if (i == (m_ray_count_per_direction + 1) / 2 - 1)
            {
                CreateBeam(ray_origin, base_direction, m_ray_visible_duration);
            }
        }
    }

    private IEnumerator VisualizeRay(Vector3 start, Vector3 dir_length, float duration)
    {
        // visualize ray in editor
        Debug.DrawRay(start, dir_length, Color.red, duration);

        // visualize ray in game
        GameObject line_obj = new GameObject("Ray");
        line_obj.transform.SetParent(transform);

        LineRenderer line_renderer = line_obj.AddComponent<LineRenderer>();
        line_renderer.positionCount = 2;
        line_renderer.SetPosition(0, start);
        line_renderer.SetPosition(1, start + dir_length);
        line_renderer.startWidth = 0.1f;
        line_renderer.endWidth = 0.1f;
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.startColor = Color.red;
        line_renderer.endColor = Color.red;

        yield return YieldHelper.WaitForSeconds(duration);
        Destroy(line_obj);
    }

    private void CreateBeam(Vector2 start, Vector2 dir, float duration)
    {
        GameObject beam = new GameObject("ExplosionBeam");
        beam.transform.SetParent(transform);

        GameObject top_part = new GameObject("TopPart");
        SpriteRenderer top_renderer = top_part.AddComponent<SpriteRenderer>();
        top_renderer.sprite = m_beam_top_sprite;
        top_renderer.sortingLayerName = m_sorting_layer;
        top_renderer.sortingOrder = m_order_in_sorting_layer;
        top_renderer.material = m_unlit_material;
        top_renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        top_part.transform.SetParent(beam.transform);
        top_part.transform.position = start + Vector2.left * m_explosion_range;

        GameObject stretch_part = new GameObject("StretchPart");
        SpriteRenderer stretch_renderer = stretch_part.AddComponent<SpriteRenderer>();
        stretch_renderer.sprite = m_beam_stretch_sprite;
        stretch_renderer.sortingLayerName = m_sorting_layer;
        stretch_renderer.sortingOrder = m_order_in_sorting_layer;
        stretch_renderer.material = m_unlit_material;
        stretch_renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        stretch_part.transform.SetParent(beam.transform);
        stretch_part.transform.position = start + Vector2.left * m_explosion_range / 2;

        float x_scale = m_explosion_range/ (stretch_renderer.bounds.size.x + 0.5f);
        stretch_part.transform.localScale = new Vector3(x_scale, 1.0f, 1.0f);

        float rotation = Vector3.SignedAngle(Vector3.left, dir, Vector3.forward);

        beam.transform.RotateAround(transform.position, Vector3.forward, rotation);

        StartCoroutine(DestroyBeamAfterDuration(beam, m_ray_visible_duration));
    }

    private IEnumerator DestroyBeamAfterDuration(GameObject beam, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(beam);
    }
}
