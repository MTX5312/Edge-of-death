using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; // Esto permite usar SceneAsset en el editor
#endif

public class Teleport : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset escenaDestino; // Permite elegir la escena visualmente
#endif

    [SerializeField, Tooltip("Si usás Build Settings, asegurate de que la escena esté incluida")]
    private string nombreEscenaDestino;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (escenaDestino != null)
            nombreEscenaDestino = escenaDestino.name;
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}