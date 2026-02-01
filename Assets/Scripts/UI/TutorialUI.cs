using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject tutorialRoot;
    [SerializeField] private float minVisibleTime = 6f; // z.B. 3 Frames × 2s

    private bool dismissed = false;
    private float timer = 0f;

    void Awake()
    {
        if (!tutorialRoot)
            tutorialRoot = gameObject;

        tutorialRoot.SetActive(true);
        dismissed = false;
        timer = 0f;
    }

    void Update()
    {
        if (dismissed) return;

        timer += Time.deltaTime;

        if (timer < minVisibleTime)
            return;

        if (Input.anyKeyDown ||
            Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) ||
            Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f ||
            Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f)
        {
            HideTutorial();
        }
    }

    private void HideTutorial()
    {
        dismissed = true;
        tutorialRoot.SetActive(false);
    }
}
