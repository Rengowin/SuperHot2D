using UnityEngine;
using TMPro;

public class WaveCounterUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private WaveManager waveManager;

    [Header("Normal Wave UI")]
    [SerializeField] private GameObject normalWaveRoot;   // "Current Wave" group
    [SerializeField] private TMP_Text normalWaveText;     // "Wave X / Y"

    [Header("Boss Wave UI")]
    [SerializeField] private GameObject bossWaveRoot;     // "BossWave" group
    [SerializeField] private TMP_Text bossText;           // "BOSS ROUND!"

    void Awake()
    {
        if (waveManager == null)
            waveManager = FindFirstObjectByType<WaveManager>();
    }

    void OnEnable()
    {
        if (waveManager != null)
            waveManager.onWaveChanged += OnWaveChanged;
    }

    void OnDisable()
    {
        if (waveManager != null)
            waveManager.onWaveChanged -= OnWaveChanged;
    }

    private void OnWaveChanged(int currentWave, int totalWaves, bool bossRound)
    {
        // Toggle which UI shows
        if (normalWaveRoot) normalWaveRoot.SetActive(!bossRound);
        if (bossWaveRoot) bossWaveRoot.SetActive(bossRound);

        // Update text
        if (!bossRound)
        {
            if (normalWaveText)
                normalWaveText.text = $"Wave {currentWave} / {totalWaves}";
        }
        else
        {
            if (bossText)
                bossText.text = "BOSS ROUND!";
        }
    }

    public void HideAll()
    {
        if (normalWaveRoot) normalWaveRoot.SetActive(false);
        if (bossWaveRoot) bossWaveRoot.SetActive(false);
    }
}
