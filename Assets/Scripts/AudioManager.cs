using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip damagedSound;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (!musicSource)
            musicSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        PlayMenuMusic();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameEnded) return;

        if (scene.name == "MainMenu")
            PlayMenuMusic();
        else
            PlayGameplayMusic();
    }

    public void PlayMenuMusic()
    {
        gameEnded = false;
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        gameEnded = false;
        PlayMusic(gameplayMusic);
    }

    public void StopMusic()
    {
        if (musicSource)
            musicSource.Stop();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayWin()
    {
        gameEnded = true;
        StopMusic();

        if (sfxSource && winSound)
            sfxSource.PlayOneShot(winSound);
    }

    public void PlayLose()
    {
        gameEnded = true;
        StopMusic();

        if (sfxSource && loseSound)
            sfxSource.PlayOneShot(loseSound);
    }

    public void PlayDamaged()
    {
        if (sfxSource && damagedSound)
            sfxSource.PlayOneShot(damagedSound);
    }
}
