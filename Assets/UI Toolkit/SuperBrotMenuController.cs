using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "GameScene"; // Set this to your game scene name in Inspector

    private VisualElement root;
    private VisualElement settingsPanel;

    private Button newGameButton;
    private Button settingsButton;
    private Button exitButton;
    private Button closeButton;
    private Button applyButton;

    private Slider musicSlider;
    private Slider sfxSlider;

    private AudioManager audioManager;

    void Awake()
    {
        Debug.Log("=== MainMenuController Awake started ===");
        
        var uiDoc = GetComponent<UIDocument>();
        if (uiDoc == null)
        {
            Debug.LogError("UIDocument not found!");
            return;
        }
        
        root = uiDoc.rootVisualElement;
        
        // Get AudioManager reference
        audioManager = GetComponent<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found on the same GameObject!");
        }

        // Get all UI elements
        settingsPanel = root.Q<VisualElement>("settings-panel");
        newGameButton = root.Q<Button>("NewGameButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");
        closeButton = root.Q<Button>("close-btn");
        applyButton = root.Q<Button>("apply-btn");
        musicSlider = root.Q<Slider>("music-slider");
        sfxSlider = root.Q<Slider>("sfx-slider");

        // Debug: Check if elements were found
        Debug.Log($"New Game Button found: {newGameButton != null}");
        Debug.Log($"Settings Button found: {settingsButton != null}");
        Debug.Log($"Exit Button found: {exitButton != null}");
        Debug.Log($"Settings Panel found: {settingsPanel != null}");

        // Initialize panel as hidden
        if (settingsPanel != null)
        {
            settingsPanel.RemoveFromClassList("visible");
            settingsPanel.style.display = DisplayStyle.None;
        }

        // Load current volumes into sliders
        if (audioManager != null && musicSlider != null && sfxSlider != null)
        {
            musicSlider.value = audioManager.BgmVolume * 100f;
            sfxSlider.value = audioManager.SfxVolume * 100f;
        }

        // Register all button callbacks
        if (newGameButton != null)
        {
            newGameButton.clicked += OnNewGame;
            Debug.Log("New Game button callback registered");
        }
        
        if (settingsButton != null)
        {
            settingsButton.clicked += OpenSettings;
            Debug.Log("Settings button callback registered");
        }
        
        if (exitButton != null)
        {
            exitButton.clicked += OnExit;
            Debug.Log("Exit button callback registered");
        }
        
        if (closeButton != null)
        {
            closeButton.clicked += CloseSettings;
        }
        
        if (applyButton != null)
        {
            applyButton.clicked += ApplySettings;
        }

        Debug.Log("=== Setup complete ===");
    }

    private void OnNewGame()
    {
        Debug.Log("New Game clicked!");
        
        // Play click sound
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }

        // Load the game scene
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("Game scene name not set! Please set it in the Inspector.");
        }
    }

    private void OnExit()
    {
        Debug.Log("Exit clicked!");
        
        // Play click sound
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }

        // Exit the application
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OpenSettings()
    {
        Debug.Log("OpenSettings called!");
        
        if (settingsPanel != null)
        {
            settingsPanel.style.display = DisplayStyle.Flex;
            // Force a small delay to ensure display change happens before animation
            settingsPanel.schedule.Execute(() => 
            {
                settingsPanel.AddToClassList("visible");
            }).StartingIn(10);
        }
        
        // Play click sound
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
    }

    private void CloseSettings()
    {
        Debug.Log("CloseSettings called!");
        
        if (settingsPanel != null)
        {
            settingsPanel.RemoveFromClassList("visible");
            
            // Wait for animation to finish before hiding
            settingsPanel.schedule.Execute(() => 
            {
                settingsPanel.style.display = DisplayStyle.None;
            }).StartingIn(300); // Match transition duration in CSS
        }
        
        // Play click sound
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
    }

    private void ApplySettings()
    {
        Debug.Log("ApplySettings called!");
        
        if (audioManager != null && musicSlider != null && sfxSlider != null)
        {
            float musicVolume = musicSlider.value / 100f;
            float sfxVolume = sfxSlider.value / 100f;

            audioManager.BgmVolume = musicVolume;
            audioManager.SfxVolume = sfxVolume;
            audioManager.SaveVolumes();

            Debug.Log($"Applied - Music: {musicVolume}, SFX: {sfxVolume}");
            
            // Play click sound
            audioManager.PlayClickSound();
        }
    }
}