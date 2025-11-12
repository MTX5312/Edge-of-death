using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMetrics : MonoBehaviour
{
    private int fallCount = 0;
    private float maxSpeed = 0f;
    private float totalSpeed = 0f;
    private int speedSamples = 0;

    private string filePath;

    private void Start()
    {
        filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/Metrics.txt";

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "===== MÉTRICAS DEL JUEGO =====\n\n");
        }
    }

    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            float currentSpeed = rb.velocity.magnitude;

            if (currentSpeed > maxSpeed)
                maxSpeed = currentSpeed;

            totalSpeed += currentSpeed;
            speedSamples++;
        }
    }

    // 🔹 Este método se llama cada vez que el jugador toca una zona de muerte
    public void RegisterFall()
    {
        fallCount++;
        Debug.Log("📉 Caída registrada. Total: " + fallCount);
    }

    // 🔹 Guardar métricas en archivo
    private void SaveMetrics()
    {
        float averageSpeed = speedSamples > 0 ? totalSpeed / speedSamples : 0;
        string sceneName = SceneManager.GetActiveScene().name;

        string data =
            "----- NUEVA SESIÓN -----\n" +
            "Fecha: " + System.DateTime.Now + "\n" +
            "Escena: " + sceneName + "\n" +
            "Caídas: " + fallCount + "\n" +
            "Velocidad Máxima: " + maxSpeed.ToString("F2") + "\n" +
            "Velocidad Promedio: " + averageSpeed.ToString("F2") + "\n\n";

        File.AppendAllText(filePath, data);
        Debug.Log("✅ Métricas guardadas en: " + filePath);
    }

    private void OnApplicationQuit()
    {
        SaveMetrics();
    }

#if UNITY_EDITOR
    //🔹 También guarda al salir del modo Play en el Editor
    private void OnDestroy()
    {
        if (!Application.isPlaying)
            return;

        SaveMetrics();
    }
#endif
}