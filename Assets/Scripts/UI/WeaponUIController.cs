using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponController weaponController;

    [Header("UI")]
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Weapon Icons")]
    [SerializeField] private Sprite pistolIcon;
    [SerializeField] private Sprite shotgunIcon;
    [SerializeField] private Sprite rocketIcon;
    [SerializeField] private Sprite spearIcon;
    [SerializeField] private Sprite swordsIcon;
    [SerializeField] private Sprite combinedIcon;

    void Awake()
    {
        if (!weaponController)
            weaponController = FindFirstObjectByType<WeaponController>();
    }

    void OnEnable()
    {
        if (!weaponController) return;

        weaponController.onWeaponChanged += OnWeaponChanged;
        weaponController.onAmmoChanged += OnAmmoChanged;
    }

    void OnDisable()
    {
        if (!weaponController) return;

        weaponController.onWeaponChanged -= OnWeaponChanged;
        weaponController.onAmmoChanged -= OnAmmoChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        if (weaponIcon == null) return;

        if (weapon is Pistol)
            weaponIcon.sprite = pistolIcon;
        else if (weapon is ShotGun)
            weaponIcon.sprite = shotgunIcon;
        else if (weapon is RocketLauncher)
            weaponIcon.sprite = rocketIcon;
        else if (weapon is Spear)
            weaponIcon.sprite = spearIcon;
        else if (weapon is Swords)
            weaponIcon.sprite = swordsIcon;
        else
            weaponIcon.sprite = combinedIcon;
    }

    private void OnAmmoChanged(int current, int max)
    {
        if (ammoText == null) return;

        if (current < 0)
            ammoText.text = ""; // melee weapon
        else
            ammoText.text = $"{current} / {max}";
    }
}
