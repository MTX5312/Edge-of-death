using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Nivel 1_Limbo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
