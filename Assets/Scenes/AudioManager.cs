using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Música del Menú")]
    [SerializeField] private AudioClipWithVolume menuMusic;

    [Header("Música específica por nivel (Dante Edition)")]
    [SerializeField] private LevelMusic[] levelMusics = new LevelMusic[0];

    [Header("Música por defecto si no hay asignada")]
    [SerializeField] private AudioClipWithVolume defaultLevelMusic;

    private AudioSource musicSource;

    [System.Serializable]
    public class AudioClipWithVolume
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 0.3f;
    }

    [System.Serializable]
    public class LevelMusic
    {
        public string levelNameContains;   // ? Solo pones una parte del nombre (ej: "Limbo", "Lujuria", etc.)
        public AudioClipWithVolume music;
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
        else Destroy(gameObject);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        // MENÚ
        if (sceneName.Contains("Menu") || sceneName.Contains("Main"))
        {
            PlayMusic(menuMusic);
            return;
        }

        // BUSCA COINCIDENCIA PARCIAL EN TUS NIVELES
        var match = levelMusics.FirstOrDefault(lm => sceneName.Contains(lm.levelNameContains));

        if (match != null && match.music.clip != null)
            PlayMusic(match.music);
        else
            PlayMusic(defaultLevelMusic); // por si añades un nivel nuevo sin música aún
    }

    void PlayMusic(AudioClipWithVolume music)
    {
        if (music?.clip == null) return;

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
}