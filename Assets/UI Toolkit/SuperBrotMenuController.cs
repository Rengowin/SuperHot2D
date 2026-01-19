using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<VisualElement>("newGame")
            .RegisterCallback<ClickEvent>(_ => StartGame());

        root.Q<VisualElement>("settings")
            .RegisterCallback<ClickEvent>(_ => OpenSettings());

        root.Q<VisualElement>("exit")
            .RegisterCallback<ClickEvent>(_ => Quit());
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void OpenSettings()
    {
        Debug.Log("Settings");
    }

    void Quit()
    {
        Application.Quit();
    }
}
