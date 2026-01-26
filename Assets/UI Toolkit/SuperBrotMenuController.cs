using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Level1";

    void Awake()
    {
        Debug.Log("[MainMenu] Awake running");

        var uiDoc = GetComponent<UIDocument>();
        if (uiDoc == null)
        {
            Debug.LogError("[MainMenu] UIDocument missing on this GameObject!");
            return;
        }

        var root = uiDoc.rootVisualElement;

        var newGameBtn = root.Q<Button>("NewGameButton");
        var newGameLbl = root.Q<Label>("NewGame");
        var settingsBtn = root.Q<Button>("SettingsButton");
        var exitBtn = root.Q<Button>("ExitButton");

        Debug.Log($"[MainMenu] Buttons found? newGame={newGameBtn != null}, settings={settingsBtn != null}, exit={exitBtn != null}");

        if (newGameBtn != null) newGameBtn.clicked += StartGame;
        if (newGameLbl != null) newGameLbl.RegisterCallback<ClickEvent>(_ => StartGame());
        if (settingsBtn != null) settingsBtn.clicked += OpenSettings;
        if (exitBtn != null) exitBtn.clicked += Quit;
    }

    void StartGame()
    {
        Debug.Log($"[MainMenu] StartGame clicked -> loading scene '{gameplaySceneName}'");

        // Print scenes in build (helps catch name mismatches)
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            Debug.Log($"[MainMenu] BuildScene[{i}] = {path}");
        }

        SceneManager.LoadScene(gameplaySceneName);
    }

    void OpenSettings() => Debug.Log("[MainMenu] Settings clicked");
    void Quit() 
    { 
        Debug.Log("[MainMenu] Exit clicked");  
        Application.Quit(); 
    }
}
