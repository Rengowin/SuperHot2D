using UnityEngine;
using TMPro;

public class AmmoCounterUI : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private GameObject root; 

    void Awake()
    {
        if (weaponController == null)
            weaponController = FindFirstObjectByType<WeaponController>();

        if (ammoText == null)
            ammoText = GetComponentInChildren<TMP_Text>();

        if (root == null)
            root = gameObject;
    }

    void OnEnable()
    {
        if (weaponController != null)
            weaponController.onAmmoChanged += UpdateAmmoUI;
    }

    void OnDisable()
    {
        if (weaponController != null)
            weaponController.onAmmoChanged -= UpdateAmmoUI;
    }

    private void UpdateAmmoUI(int current, int max)
    {
        // No-ammo weapons (melee etc.)
        if (current < 0 || max < 0)
        {
            if (root) root.SetActive(false);
            return;
        }

        if (root) root.SetActive(true);

        if (ammoText != null)
            ammoText.text = $"{current} / {max}";
    }
}
