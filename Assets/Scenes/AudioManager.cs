using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips con su propio volumen")]
    [SerializeField] private AudioClipWithVolume menuMusic;
    [SerializeField] private AudioClipWithVolume gameMusic;

    private AudioSource musicSource;

    [System.Serializable]
    public class AudioClipWithVolume
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 0.3f;   
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = GetComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Menu") || scene.name.Contains("Main"))
            PlayMusic(menuMusic);
        else
            PlayMusic(gameMusic);
    }

    void PlayMusic(AudioClipWithVolume music)
    {
        if (musicSource.clip != music.clip || !musicSource.isPlaying)
        {
            musicSource.clip = music.clip;
            musicSource.volume = music.volume;
            musicSource.Play();
        }
        else
        {
            musicSource.volume = music.volume;  
        }
    }

    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Max(volume, 0.001f);  
    }
}