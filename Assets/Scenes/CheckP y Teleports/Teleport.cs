using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPuerta : MonoBehaviour
{
    [SerializeField] private string nombreSiguienteEscena;
    [SerializeField] private bool usarIndice = false;
    [SerializeField] private int indiceSiguienteEscena = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CargarSiguienteEscena();
        }
    }

    private void CargarSiguienteEscena()
    {
        if (usarIndice)
        {
            SceneManager.LoadScene(indiceSiguienteEscena);
        }
        else
        {
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}