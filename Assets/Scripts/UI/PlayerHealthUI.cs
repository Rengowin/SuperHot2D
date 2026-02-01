using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerStats player;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    void Awake()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerStats>();

        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = 1f;
        }
    }

    void OnEnable()
    {
        if (player != null)
            player.onHealthChanged += OnHealthChanged;
    }

    void OnDisable()
    {
        if (player != null)
            player.onHealthChanged -= OnHealthChanged;
    }

    void Start()
    {
        // force initial update
        if (player != null)
            OnHealthChanged(player.HP, player.MaxHP);
    }

    private void OnHealthChanged(float current, float max)
    {
        float t = (max <= 0f) ? 0f : current / max;

        if (healthSlider != null)
            healthSlider.value = t;

        if (healthText != null)
            healthText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }
}