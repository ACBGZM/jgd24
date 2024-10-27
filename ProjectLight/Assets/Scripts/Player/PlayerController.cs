using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public enum PlayerStatus
{
    default_status = 0,
    planting_a_bomb = 1,
    moving = 2,
    idle = 3,
    on_hit = 4,
}

[Serializable]
public class PlayerItem
{
    public PlayerItem(int id, uint count)
    {
        m_count = count;
        m_item_id = id;
    }

    public int m_item_id;
    public uint m_count;
    public GameObject m_prefab;
    public Sprite m_ui_sprite;
}

public class PlayerController : MonoBehaviour
{
    // component
    private Rigidbody2D m_rigidbody;

    // input
    private PlayerInput m_player_input;
    private InputAction m_plant_a_bomb_action;
    private InputAction m_detonate_all_bombs_action;
    private InputAction m_switch_item_action;

    private InputAction m_pause_game_action;

    [SerializeField]
    private UILogic m_ui_logic;

    // movement
    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
    ]
    private Vector2 m_input_movement;

    [SerializeField]
    private float m_speed;

    [SerializeField]
    private float m_speed_while_planting;

    // planting bomb
    private LineRenderer m_planting_progress_line;
    private const float m_planting_progress_line_length = 2.0f;
    private const float m_planting_progress_line_width = 0.3f;
    private const float m_planting_progress_line_height = 0.8f;

    private const double m_planting_duration = 0.6f; // todo
    private double m_planting_start_time;

    // switch bomb
    [SerializeField] private List<PlayerItem> m_items;

    public void AddItem(int item_id)
    {
        PlayerItem item = m_items.Find(t => t.m_item_id == item_id);
        if (item != null)
        {
            ++item.m_count;
        }
        else
        {
            m_items.Add(new PlayerItem(item_id, 1));
        }

        m_ui_logic.UpdateCurrentItem(m_items.Find(t => t.m_item_id == m_current_item));
    }

    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
        ]
    private int m_current_item = -1;

    // animation
    private PlayerAnimationController m_animation_controller = null;
    private PlayerOnHit m_player_on_hit = null;

    // status
    [SerializeField
#if UNITY_EDITOR
        , ReadOnly
#endif
    ]
    private PlayerStatus m_status = PlayerStatus.default_status;

    public PlayerStatus getPlayerStatus() => m_status;

    public void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_player_input = new PlayerInput();
        m_plant_a_bomb_action = m_player_input.GP.PlantBomb;
        m_detonate_all_bombs_action = m_player_input.GP.DetonateBomb;
        m_pause_game_action = m_player_input.GP.PauseGame;
        m_switch_item_action = m_player_input.GP.SwitchItem;
    }

    private void OnEnable()
    {
        m_player_input.Enable();

        m_plant_a_bomb_action.performed += PlantABombPerformed;
        m_plant_a_bomb_action.started += PlantABombStarted;
        m_plant_a_bomb_action.canceled += PlantABombCanceled;

        m_detonate_all_bombs_action.performed += DetonateAllBombs;
        m_switch_item_action.performed += SwitchItem;

        m_pause_game_action.performed += PauseGame;
    }

    private void OnDisable()
    {
        m_plant_a_bomb_action.performed -= PlantABombPerformed;
        m_plant_a_bomb_action.started -= PlantABombStarted;
        m_plant_a_bomb_action.canceled -= PlantABombCanceled;

        m_detonate_all_bombs_action.performed -= DetonateAllBombs;
        m_switch_item_action.performed -= SwitchItem;

        m_pause_game_action.performed -= PauseGame;

        m_player_input.Disable();
    }

    private void Start()
    {
        m_planting_progress_line = gameObject.AddComponent<LineRenderer>();
        m_planting_progress_line.startWidth = m_planting_progress_line_width;
        m_planting_progress_line.endWidth = m_planting_progress_line_width;
        m_planting_progress_line.positionCount = 2;
        m_planting_progress_line.startColor = Color.yellow;
        m_planting_progress_line.endColor = Color.yellow;
        m_planting_progress_line.material = new Material(Shader.Find("Sprites/Default"));
        m_planting_progress_line.enabled = false;

        m_planting_progress_line.sortingLayerName =  "Player";
        m_planting_progress_line.sortingOrder = 30;

        m_animation_controller = GetComponentInChildren<PlayerAnimationController>();
        Debug.Log(m_animation_controller);
        m_animation_controller.SetResetHitAction(ResetOnHitStatus);

        m_player_on_hit = GetComponent<PlayerOnHit>();
        m_player_on_hit.SetOnHitAction(SetOnHitStatus);

        m_player_on_hit.OnDead += Dead;

        m_current_item = m_items.First().m_item_id;
        m_ui_logic.UpdateCurrentItem(m_items.Find(t => t.m_item_id == m_current_item));
    }

    private bool m_is_on_hit_last_frame = false;

    public void Update()
    {
        if (m_status == PlayerStatus.on_hit)
        {
            if (!m_is_on_hit_last_frame)
            {
                DisableInput();
            }

            m_is_on_hit_last_frame = true;
            return;
        }
        else
        {
            if (m_is_on_hit_last_frame)
            {
                m_player_input.Enable();
            }

            m_is_on_hit_last_frame = false;
        }

        ProcessInput();
        DrawPlantingProgressLine();
        SetAnimation();
    }

    public void FixedUpdate()
    {
        if (m_status == PlayerStatus.on_hit)
        {
            DisableInput();
            return;
        }
        else
        {
            m_player_input.Enable();
        }

        PlayerMovement();
    }

    private void ProcessInput()
    {
        m_input_movement = m_player_input.GP.Move.ReadValue<Vector2>();
        m_input_movement = m_player_input.GP.Move.ReadValue<Vector2>();
    }

    private void PlayerMovement()
    {
        if (m_input_movement.x != 0 || m_input_movement.y != 0)
        {
            float2 movement_input = math.normalize(
                new float2(m_input_movement.x, m_input_movement.y)
            );
            float speed =
                m_status == PlayerStatus.planting_a_bomb ? m_speed_while_planting : m_speed;
            m_rigidbody.MovePosition(
                m_rigidbody.position
                    + (new Vector2(movement_input.x, movement_input.y)) * speed * Time.deltaTime
            );

            if (m_input_movement.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (m_input_movement.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            if (m_status != PlayerStatus.planting_a_bomb)
            {
                m_status = PlayerStatus.moving;
            }
        }
        else if (m_status != PlayerStatus.planting_a_bomb)
        {
            m_status = PlayerStatus.idle;
        }
    }

    private void PlantABombPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        PlayerItem current_item = m_items.Find(t => t.m_item_id == m_current_item);
        if (current_item == null || current_item.m_count == 0)
        {
            return;
        }

        --current_item.m_count;
        BombManager.GetInstance().PlantABomb(transform, current_item);
        m_status = PlayerStatus.default_status;

        m_ui_logic.UpdateCurrentItem(current_item);
    }

    private void PlantABombStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        PlayerItem current_item = m_items.Find(t => t.m_item_id == m_current_item);
        if (current_item == null || current_item.m_count == 0)
        {
            return;
        }

        m_status = PlayerStatus.planting_a_bomb;

        m_planting_start_time = Time.time;
    }

    private void PlantABombCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        PlayerItem current_item = m_items.Find(t => t.m_item_id == m_current_item);
        if (current_item == null || current_item.m_count == 0)
        {
            return;
        }

        if (m_status != PlayerStatus.on_hit)
        {
            m_status = PlayerStatus.default_status;
        }

        m_planting_start_time = 0f;
        m_planting_progress_line.enabled = false;
    }

    private void DetonateAllBombs(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        BombManager.GetInstance().DetonateAllBombs();
    }

    private void DrawPlantingProgressLine()
    {
        if (m_status == PlayerStatus.planting_a_bomb)
        {
            m_planting_progress_line.enabled = true;

            double planting_time = Time.time - m_planting_start_time;
            float progress_line_length = Mathf.Lerp(
                0,
                m_planting_progress_line_length,
                (float)(planting_time / m_planting_duration)
            );

            Vector3 playerHeadPosition =
                transform.position
                + new Vector3(
                    -m_planting_progress_line_length / 2.0f,
                    m_planting_progress_line_height,
                    0
                );
            m_planting_progress_line.SetPosition(0, playerHeadPosition);
            m_planting_progress_line.SetPosition(
                1,
                playerHeadPosition + Vector3.right * progress_line_length
            );
        }
        else
        {
            m_planting_progress_line.enabled = false;
        }
    }

    private void PauseGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_ui_logic.PauseGame();
        m_ui_logic.ShowPanel(true);
    }

    private void SetOnHitStatus()
    {
        m_status = PlayerStatus.on_hit;
    }

    private void ResetOnHitStatus()
    {
        m_status = PlayerStatus.default_status;
    }

    private void SetAnimation()
    {
        m_animation_controller.PlayerStatus = m_status;
    }

    private void DisableInput()
    {
        m_player_input.Disable();
        m_plant_a_bomb_action.Disable();
        m_detonate_all_bombs_action.Disable();
        m_switch_item_action.Disable();
    }

    private void Dead()
    {
        m_ui_logic.GameOver(false);
    }

    private void SwitchItem(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        SwitchItemInner();
    }

    private void SwitchItemInner()
    {
        PlayerItem current_item = m_items.Find(t => t.m_item_id == m_current_item);
        int index = m_items.IndexOf(current_item);
        if (index >= m_items.Count - 1)
        {
            m_current_item = m_items.First().m_item_id;
        }
        else
        {
            m_current_item = m_items[++index].m_item_id;
        }

        m_ui_logic.UpdateCurrentItem(m_items.Find(t => t.m_item_id == m_current_item));
    }
}
