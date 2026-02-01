using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI_Chunks : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStats player;
    [SerializeField] private Image[] healthImages; // size = 10

    private float hpPerImage;

    void Awake()
    {
        if (!player)
            player = FindFirstObjectByType<PlayerStats>();

        if (healthImages == null || healthImages.Length == 0)
        {
            Debug.LogError("Health images not assigned!");
            return;
        }

        hpPerImage = player.MaxHP / healthImages.Length;
    }

    void OnEnable()
    {
        if (player != null)
            player.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI(player.HP, player.MaxHP);
    }

    void OnDisable()
    {
        if (player != null)
            player.onHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI(float currentHP, float maxHP)
    {
        int activeImages = Mathf.CeilToInt(currentHP / hpPerImage);

        for (int i = 0; i < healthImages.Length; i++)
        {
            healthImages[i].enabled = i < activeImages;
        }
    }
}
