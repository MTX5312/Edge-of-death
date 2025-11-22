using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Menú de Pausa")]
    [SerializeField] private GameObject pausePanel;

    [Header("Submenús reutilizados del Main Menu")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject controlesMenu;

    [Header("Canvas Group (opcional pero recomendado)")]
    [SerializeField] private CanvasGroup pauseCanvasGroup;

    private void Start()
    {
        pausePanel.SetActive(false);
        optionsMenu.SetActive(false);
        controlesMenu.SetActive(false);

        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 0f;
            pauseCanvasGroup.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlesMenu.activeSelf || optionsMenu.activeSelf)
                VolverAPausa();
            else if (pausePanel.activeSelf)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        optionsMenu.SetActive(false);
        controlesMenu.SetActive(false);

        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 1f;
            pauseCanvasGroup.blocksRaycasts = true;
        }

        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        optionsMenu.SetActive(false);
        controlesMenu.SetActive(false);

        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 0f;
            pauseCanvasGroup.blocksRaycasts = false;
        }

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void OnResumeButton() => Resume();

    public void OnControlesButton()
    {
        pausePanel.SetActive(false);
        controlesMenu.SetActive(true);
    }

    public void OnOpcionesButton()
    {
        pausePanel.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnSalirAlMenuButton()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Main Menu"); 
    }

    public void VolverAPausa()
    {
        pausePanel.SetActive(true);
        optionsMenu.SetActive(false);
        controlesMenu.SetActive(false);
    }
}
