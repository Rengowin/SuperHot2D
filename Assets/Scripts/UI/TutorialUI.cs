using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject tutorialRoot;

    private bool dismissed = false;

    void Awake()
    {
        if (!tutorialRoot)
            tutorialRoot = gameObject;

        // Show tutorial at scene start
        tutorialRoot.SetActive(true);
        dismissed = false;
    }

    void Update()
    {
        if (dismissed) return;

        // Any keyboard input
        if (Input.anyKeyDown)
        {
            HideTutorial();
            return;
        }

        // Mouse click
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            HideTutorial();
            return;
        }

        // Mouse movement
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f ||
            Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f)
        {
            HideTutorial();
            return;
        }
    }

    private void HideTutorial()
    {
        dismissed = true;
        tutorialRoot.SetActive(false);
    }
}
