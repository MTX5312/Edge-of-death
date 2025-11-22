using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public Button volverButton;  // Arrastra el botón aquí

    private void Start()
    {
        // Oculta cursor si lo usas en el juego
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Tu escena del menú
    }
}
