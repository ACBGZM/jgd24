using UnityEngine;
using UnityEngine.UI;

public class HealthUIUpdater : MonoBehaviour
{
    public Slider playerHealthSlider;
    public Slider bossHealthSlider;
    public GameObject player;
    public GameObject boss;

    private void OnEnable()
    {
        HealthChangeEvent.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        HealthChangeEvent.OnHealthChanged -= UpdateHealthBar;
    }

    public void UpdateHealthBar(int newHealth, GameObject sourceObject)
    {
        if (sourceObject == player)
        {
            playerHealthSlider.maxValue = player.GetComponent<PlayerOnHit>().maxHealth;
            playerHealthSlider.value = player.GetComponent<PlayerOnHit>().Health;
        }
        else if (sourceObject == boss)
        {
            bossHealthSlider.maxValue = boss.GetComponent<BossOnHit>().maxHealth;
            bossHealthSlider.value = boss.GetComponent<BossOnHit>().Health;
        }
    }
}
