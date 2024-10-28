using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField]
    public GameObject itemPrefab;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private float timer;

    [SerializeField]
    private float minDuration = 3f;

    [SerializeField]
    private float maxDuration = 6f;

    [SerializeField]
    private float minLength = 10f;

    [SerializeField]
    private float maxLength = 50f;

    [SerializeField]
    public string[] layerMasks;
    private LayerMask layerMask;

    void Awake()
    {
        layerMask = LayerMask.GetMask(layerMasks);
    }

    void Start()
    {
        timer = Random.Range(minDuration, maxDuration);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnItem();
            timer = Random.Range(minDuration, maxDuration);
        }
    }

    void SpawnItem()
    {
        float angle = Random.Range(0f, 360f);
        float length = Random.Range(minLength, maxLength);
        Vector2 spawnDirection = Quaternion.Euler(0f, 0f, angle) * Vector2.right;
        Vector2 spawnPosition = (Vector2)player.transform.position + spawnDirection * length;

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            player.transform.position,
            spawnDirection,
            length,
            layerMask
        );
        foreach (RaycastHit2D hit in hits)
        {
            spawnPosition = hit.point;
            break;
        }

        Instantiate(itemPrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, minLength);
        Gizmos.DrawWireSphere(player.transform.position, maxLength);
    }
}
