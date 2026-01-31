using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip clickSFX;

    [Header("Default Volumes (0–1)")]
    [SerializeField, Range(0f, 1f)] private float defaultBgmVolume = 0.5f;
    [SerializeField, Range(0f, 1f)] private float defaultSfxVolume = 0.8f;

    [Header("Audio Sources (optional – auto-created if missing)")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private UIDocument uiDoc;
    private readonly List<VisualElement> interactiveElements = new();

    // Public properties – useful for settings menu later
    public float BgmVolume
    {
        get => bgmSource ? bgmSource.volume : defaultBgmVolume;
        set
        {
            if (bgmSource) bgmSource.volume = Mathf.Clamp01(value);
        }
    }

    public float SfxVolume
    {
        get => sfxSource ? sfxSource.volume : defaultSfxVolume;
        set
        {
            if (sfxSource) sfxSource.volume = Mathf.Clamp01(value);
        }
    }

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        if (uiDoc == null)
        {
            Debug.LogError($"{nameof(AudioManager)} requires a UIDocument component on the same GameObject.", this);
            enabled = false;
            return;
        }

        InitializeAudioSources();
        LoadSavedVolumes();
    }

    private void Start()
    {
        PlayBackgroundMusicIfPossible();
        RegisterInteractiveElements();
    }

    private void OnDestroy()
    {
        // Optional: save volumes when leaving the scene
        SaveVolumes();
    }

    private void InitializeAudioSources()
    {
        if (bgmSource == null)
        {
            var go = new GameObject("BGM_Source") { transform = { parent = transform } };
            bgmSource = go.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            var go = new GameObject("SFX_Source") { transform = { parent = transform } };
            sfxSource = go.AddComponent<AudioSource>();
        }

        // Common setup
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        sfxSource.playOnAwake = false;
        sfxSource.loop = false;

        // Apply current volumes
        BgmVolume = BgmVolume; // re-apply (handles load case)
        SfxVolume = SfxVolume;
    }

    private void PlayBackgroundMusicIfPossible()
    {
        if (backgroundMusic == null || bgmSource.isPlaying)
            return;

        bgmSource.clip = backgroundMusic;
        bgmSource.Play();
    }

    private void RegisterInteractiveElements()
    {
        interactiveElements.Clear();

        var root = uiDoc.rootVisualElement;

        var classes = new[] { "bun-top", "settings-btn", "bun-bottom" };

        foreach (var className in classes)
        {
            interactiveElements.AddRange(root.Query<VisualElement>().Class(className).ToList());
        }

        foreach (var element in interactiveElements)
        {
            element.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            element.RegisterCallback<PointerDownEvent>(OnPointerDown);
        }
    }

    private void OnPointerEnter(PointerEnterEvent evt)
    {
        if (hoverSFX != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(hoverSFX);
        }
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        // Only left click
        if (evt.button == 0 && clickSFX != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clickSFX);
        }
    }

    // ────────────────────────────────────────────────
    //  Volume Persistence (PlayerPrefs)
    // ────────────────────────────────────────────────

    private const string PREF_BGM_VOLUME = "Audio_BGM_Volume";
    private const string PREF_SFX_VOLUME = "Audio_SFX_Volume";

    private void LoadSavedVolumes()
    {
        BgmVolume = PlayerPrefs.GetFloat(PREF_BGM_VOLUME, defaultBgmVolume);
        SfxVolume = PlayerPrefs.GetFloat(PREF_SFX_VOLUME, defaultSfxVolume);
    }

    public void SaveVolumes()
    {
        PlayerPrefs.SetFloat(PREF_BGM_VOLUME, BgmVolume);
        PlayerPrefs.SetFloat(PREF_SFX_VOLUME, SfxVolume);
        PlayerPrefs.Save();
    }

    // ────────────────────────────────────────────────
    //  Public API – call from other scripts / settings
    // ────────────────────────────────────────────────

    public void PlayHover() => OnPointerEnter(null);
public void PlayClickSound()
{
    if (clickSFX != null && sfxSource != null)
    {
        sfxSource.PlayOneShot(clickSFX);
    }
}
    public void StopAllAudio()
    {
        if (bgmSource) bgmSource.Stop();
        if (sfxSource) sfxSource.Stop();
    }

    public void PauseBGM() => bgmSource?.Pause();
    public void ResumeBGM() => bgmSource?.UnPause();
}