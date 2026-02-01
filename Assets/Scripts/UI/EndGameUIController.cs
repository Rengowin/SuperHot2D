using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerStats player;
    [SerializeField] private WaveManager waveManager;

    [Header("UI Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Buttons")]
    [SerializeField] private Button winRetryButton;
    [SerializeField] private Button winMenuButton;
    [SerializeField] private Button loseRetryButton;
    [SerializeField] private Button loseMenuButton;

    [Header("Scenes")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void Awake()
    {
        if (!player) player = FindFirstObjectByType<PlayerStats>();
        if (!waveManager) waveManager = FindFirstObjectByType<WaveManager>();

        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

        // Wire buttons
        if (winRetryButton) winRetryButton.onClick.AddListener(Retry);
        if (loseRetryButton) loseRetryButton.onClick.AddListener(Retry);

        if (winMenuButton) winMenuButton.onClick.AddListener(GoToMenu);
        if (loseMenuButton) loseMenuButton.onClick.AddListener(GoToMenu);
    }

    void OnEnable()
    {
        if (player != null)
            player.onDeath.AddListener(OnPlayerDied);

        if (waveManager != null)
            waveManager.onAllWavesComplete.AddListener(OnAllWavesComplete);
    }

    void OnDisable()
    {
        if (player != null)
            player.onDeath.RemoveListener(OnPlayerDied);

        if (waveManager != null)
            waveManager.onAllWavesComplete.RemoveListener(OnAllWavesComplete);
    }

    private void OnPlayerDied()
    {
        ShowLose();
    }

    private void OnAllWavesComplete()
    {
        ShowWin();
    }

    private void ShowWin()
    {
        Time.timeScale = 0f;
        if (winPanel) winPanel.SetActive(true);
        if (losePanel) losePanel.SetActive(false);
    }

    private void ShowLose()
    {
        Time.timeScale = 0f;
        if (losePanel) losePanel.SetActive(true);
        if (winPanel) winPanel.SetActive(false);
    }

    private void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
