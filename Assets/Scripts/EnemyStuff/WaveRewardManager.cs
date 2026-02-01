using UnityEngine;

public class WaveRewardManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private WaveRewardUI rewardUI;

    [Header("Odd Wave Upgrades")]
    [SerializeField] private float hpUpgradeAmount = 20f;
    [SerializeField] private float damageUpgradeAmount = 0.2f; // +20%

    [Header("Even Wave Unlocks")]
    [SerializeField] private WeaponUnlockByWave[] unlocks;

    [Header("Player Upgrades Pool")]
    [SerializeField] PlayerStatsUpgrades playerStatsUpgrades;

    [System.Serializable]
    public class WeaponUnlockByWave
    {
        public int waveNumber;          // 2,4,6...
        public WeaponsEnum weaponType;
        public WeaponStats weaponStats; // stats to create weapon
        public Sprite weaponIcon;       // for UI
        public string displayName;      // "Shotgun"
    }

    void Awake()
    {
        if (!waveManager) waveManager = FindFirstObjectByType<WaveManager>();
        if (!playerStats) playerStats = FindFirstObjectByType<PlayerStats>();
        if (!weaponController) weaponController = FindFirstObjectByType<WeaponController>();
        if (!rewardUI) rewardUI = FindFirstObjectByType<WaveRewardUI>();
        if (!playerStatsUpgrades) playerStatsUpgrades = FindFirstObjectByType<PlayerStatsUpgrades>();
    }

    void OnEnable()
    {
        if (waveManager != null)
            waveManager.onWaveComplete.AddListener(OnWaveComplete);
    }

    void OnDisable()
    {
        if (waveManager != null)
            waveManager.onWaveComplete.RemoveListener(OnWaveComplete);
    }

    private void OnWaveComplete()
    {
        int completedWave = waveManager.CurrentWaveNumber;

        // Odd wave => show upgrade choice
        if (completedWave % 2 == 1)
        {
            var choices = playerStatsUpgrades.GetRandomUpgrades(2);

            if (choices.Count < 2)
            {
                Debug.LogWarning("Not enough upgrades in pool (need at least 2).");
                return;
            }

            var a = choices[0];
            var b = choices[1];

            string aName = FormatUpgradeText(a);
            string bName = FormatUpgradeText(b);

            rewardUI.ShowUpgradePanel(
                $"Wave {completedWave} Complete! Choose an upgrade:",
                aName, () => playerStatsUpgrades.ApplyUpgrade(a),
                bName, () => playerStatsUpgrades.ApplyUpgrade(b)
            );
            return;
        }

            // Even wave => unlock weapon + show panel
            foreach (var u in unlocks)
            {
            if (u.waveNumber != completedWave) continue;

            weaponController.UnlockWeapon(u.weaponType, u.weaponStats);

            string name = string.IsNullOrEmpty(u.displayName) ? u.weaponType.ToString() : u.displayName;

            rewardUI.ShowUnlockPanel(
                $"Unlocked: {name}",
                u.weaponIcon,
                continueAction: () => { /* nothing needed, UI resumes time */ }
            );

            return;
            }
    }


    private string BuildFallbackName(UpgradeStats u)
    {
        string sign = u.ModiferType == ModiferType.Add ? "+" : "x";
        string val = u.Value.ToString("0.##");

        if (u.Target == UpgradeTarget.Player)
            return $"{sign}{val} {u.PlayerType}";
        else
            return $"{sign}{val} {u.WeaponType}";
    }

    private string FormatUpgradeText(UpgradeStats u)
    {
        if (u == null) return "NULL UPGRADE";

        // Add => "+", Multiply => "x"
        string sign = u.ModiferType == ModiferType.Add ? "+" : "x";

        // formatiert Wert schön: 10 -> "10", 1.1 -> "1.1", 1.234 -> "1.23"
        string valueText = u.Value.ToString("0.##");

        // Ziel-Name: PlayerType oder WeaponType
        string typeText;
        if (u.Target == UpgradeTarget.Player)
            typeText = u.PlayerType.ToString();
        else
            typeText = u.WeaponType.ToString();

        // Wenn DisplayName gesetzt ist, nutze es und hänge den Wert an
        if (!string.IsNullOrEmpty(u.DisplayName))
        {
            // Beispiel: "+ HP (+10)" oder "DMG (x1.2)"
            return $"{u.DisplayName} ({sign}{valueText})";
        }

        // Fallback: "+10 Damage" oder "x1.1 MaxHealth"
        return $"{sign}{valueText} {typeText}";
    }

}
