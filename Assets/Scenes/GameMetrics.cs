using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMetrics : MonoBehaviour
{
    private int fallCount = 0;
    private float maxSpeed = 0f;
    private float totalSpeed = 0f;
    private int speedSamples = 0;

    private string folderPath;
    private string filePath;

    private void Start()
    {
        // 🔹 Crear carpeta "Metrics" en la raíz del proyecto
        folderPath = Path.Combine(Application.dataPath, "..", "Metrics");
        filePath = Path.Combine(folderPath, "Metrics.txt");

        // Crear la carpeta si no existe
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Crear archivo si no existe
        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "===== MÉTRICAS DEL JUEGO =====\n\n");

        Debug.Log("📁 Carpeta de métricas: " + Path.GetFullPath(folderPath));
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

    public void RegisterFall()
    {
        fallCount++;
        Debug.Log("📉 Caída registrada. Total: " + fallCount);
    }

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
    private void OnDestroy()
    {
        if (!Application.isPlaying)
            return;

        SaveMetrics();
    }
#endif
}