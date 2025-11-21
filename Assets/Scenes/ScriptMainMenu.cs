using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToOpcionesMenu()
    {
        SceneManager.LoadScene("OpcionesMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToControlesMenu()
    {
        SceneManager.LoadScene("ControlesMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

