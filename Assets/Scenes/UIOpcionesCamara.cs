using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOpcionesCamara : MonoBehaviour
{
    public ScriptCamara camara;  // ← Ahora SÍ es la Main Camera
    public Slider slider;
    public TextMeshProUGUI texto;

    void Start()
    {
        if (camara != null)
        {
            slider.minValue = 0.5f;
            slider.maxValue = 2.5f;
            slider.value = 1f;
            ActualizarTexto(1f);
            camara.SetSensibilidad(1f); // Valor inicial
        }
    }

    public void OnSliderChanged(float valor)
    {
        if (camara != null)
        {
            camara.SetSensibilidad(valor);
            ActualizarTexto(valor);
        }
    }

    void ActualizarTexto(float valor)
    {
        if (texto != null)
            texto.text = $"Sensibilidad: {valor:F1}x";
    }
}