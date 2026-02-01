using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SwitchBetweenTheTutorialFrames : MonoBehaviour
{
    [SerializeField]
    List<Sprite> tutorialFrames; // Changed from Image to Sprite
    [SerializeField]
    Image displayImage; // UI Image to display the sprites
    [SerializeField]
    float timeBetweenFrames;

    [SerializeField]
    bool done = false;

    private void Start()
    {
        if (tutorialFrames == null || tutorialFrames.Count == 0)
        {
            Debug.LogWarning("Keine Tutorial Frames gesetzt!");
            return;
        }

        if (displayImage == null)
        {
            Debug.LogError("Display Image fehlt!");
            return;
        }

        displayImage.gameObject.SetActive(false);
        StartCoroutine(SwitchFrames());
    }

    System.Collections.IEnumerator SwitchFrames()
    {
        foreach (var frame in tutorialFrames)
        {
            displayImage.sprite = frame; // Set the sprite to the Image component
            displayImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeBetweenFrames);
            displayImage.gameObject.SetActive(false);
        }
    }
}
