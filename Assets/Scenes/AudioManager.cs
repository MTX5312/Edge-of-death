using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // Singleton fácil

    [Header("Clips de Música (arrastra aquí)")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("Configuración")]
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        // Singleton: Solo queda 1 en toda la vida del juego
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persiste entre escenas

            // Crea el AudioSource si no lo tienes
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }

            musicSource.playOnAwake = false;
            musicSource.loop = true;
            musicSource.volume = 0.35f;  // Ajusta como quieras
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cambia música según la escena (¡automático!)
        if (scene.name == "Main Menu" || scene.name.Contains("Menu"))  // Ajusta si tu menú se llama diferente
        {
            PlayMenuMusic();
        }
        else
        {
            PlayGameMusic();
        }
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
}
