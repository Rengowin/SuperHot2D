using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveRewardUI : MonoBehaviour
{
    [Header("Upgrade Panel")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button damageButton;
    [SerializeField] private TMP_Text upgradeTitleText;

    [Header("Unlock Panel")]
    [SerializeField] private GameObject unlockPanel;
    [SerializeField] private TMP_Text unlockedNameText;
    [SerializeField] private Image unlockedIcon;
    [SerializeField] private Button continueButton;

    [SerializeField] TMP_Text hpButtonText;
    [SerializeField] TMP_Text damageButtonText;

    private System.Action onHp;
    private System.Action onDmg;
    private System.Action onContinue;

    void Awake()
    {
        if (upgradePanel) upgradePanel.SetActive(false);
        if (unlockPanel) unlockPanel.SetActive(false);

        if (hpButton) hpButton.onClick.AddListener(() => { onHp?.Invoke(); CloseAll(); });
        if (damageButton) damageButton.onClick.AddListener(() => { onDmg?.Invoke(); CloseAll(); });
        if (continueButton) continueButton.onClick.AddListener(() => { onContinue?.Invoke(); CloseAll(); });
    }

    public void ShowUpgradePanel(string title,
    string optionAText, System.Action optionA,
    string optionBText, System.Action optionB)
    {
        CloseAll();
        Time.timeScale = 0f;

        onHp = optionA;
        onDmg = optionB;

        upgradeTitleText.text = title;

        hpButton.GetComponentInChildren<TMP_Text>().text = optionAText;
        damageButton.GetComponentInChildren<TMP_Text>().text = optionBText;

        upgradePanel.SetActive(true);
    }


    public void ShowUnlockPanel(string weaponName, Sprite icon, System.Action continueAction)
    {
        CloseAll();
        Time.timeScale = 0f;

        onContinue = continueAction;

        if (unlockedNameText) unlockedNameText.text = weaponName;

        if (unlockedIcon)
        {
            unlockedIcon.sprite = icon;
            unlockedIcon.enabled = icon != null;
        }

        if (unlockPanel) unlockPanel.SetActive(true);
    }

    private void CloseAll()
    {
        if (upgradePanel) upgradePanel.SetActive(false);
        if (unlockPanel) unlockPanel.SetActive(false);

        onHp = null;
        onDmg = null;
        onContinue = null;

        Time.timeScale = 1f;
    }
}
