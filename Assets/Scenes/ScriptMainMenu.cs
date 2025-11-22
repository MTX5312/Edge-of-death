using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMainMenu : MonoBehaviour
{
    [Header("Paneles del Menú (arrastrar desde jerarquía)")]
    public GameObject mainMenu;        // Panel con Jugar, Controles, Opciones, Salir
    public GameObject optionsMenu;     // Panel de Opciones + botón Volver
    public GameObject controlesMenu;   // Panel de Controles + botón Volver

    private void Start()
    {
        MostrarMenuPrincipal();  // Al iniciar solo se ve el menú principal
    }

    // Muestra solo el menú principal
    public void MostrarMenuPrincipal()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        controlesMenu.SetActive(false);
    }

    // Abre el panel de Controles
    public void MostrarControles()
    {
        mainMenu.SetActive(false);
        controlesMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // Abre el panel de Opciones
    public void MostrarOpciones()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        controlesMenu.SetActive(false);
    }

    // Botones principales
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

