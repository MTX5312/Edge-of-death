using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOpcionesCamara : MonoBehaviour
{
    public ScriptCamara camara;
    public Slider slider;
    public TextMeshProUGUI texto;

    void Start()
    {
        slider.minValue = 0.5f;
        slider.maxValue = 2.5f;
        slider.value = 1f;
        ActualizarTexto(1f);
    }

    public void OnSliderChanged(float valor)
    {
        camara.SetSensibilidad(valor);
        ActualizarTexto(valor);
    }

    void ActualizarTexto(float valor)
    {
        texto.text = $"Sensibilidad: {valor:F1}x";
    }
}