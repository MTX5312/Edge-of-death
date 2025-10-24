using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScriptEfectosHUD : MonoBehaviour
{
    [Header("Referencias")]
    public Image dashEffect;
    public Image slideEffect;
    public ScriptJugador jugador;
    public ScriptDash dash;

    [Header("Configuración de efectos")]
    public float duracionDash = 0.2f;
    public float duracionSlide = 0.3f;
    public float alphaMaxDash = 0.6f;
    public float alphaMaxSlide = 0.6f;
    public Color colorDash = Color.white;
    public Color colorSlide = Color.cyan;

    private void Start()
    {
        // Desactivar imágenes al inicio
        if (dashEffect != null)
            dashEffect.gameObject.SetActive(false);
        if (slideEffect != null)
            slideEffect.gameObject.SetActive(false);

        // Suscribirse a eventos del dash y del jugador
        if (dash != null)
            dash.OnDash += MostrarEfectoDash;

        if (jugador != null)
            jugador.OnSlide += MostrarEfectoSlide;
    }

    private void OnDestroy()
    {
        // Desuscribirse al destruir el objeto (buena práctica)
        if (dash != null)
            dash.OnDash -= MostrarEfectoDash;

        if (jugador != null)
            jugador.OnSlide -= MostrarEfectoSlide;
    }

    private void MostrarEfectoDash()
    {
        if (dashEffect != null)
            StartCoroutine(EfectoVisual(dashEffect, colorDash, alphaMaxDash, duracionDash));
    }

    private void MostrarEfectoSlide()
    {
        if (slideEffect != null)
            StartCoroutine(EfectoVisual(slideEffect, colorSlide, alphaMaxSlide, duracionSlide));
    }

    private IEnumerator EfectoVisual(Image efecto, Color color, float alphaMax, float duracion)
    {
        efecto.gameObject.SetActive(true);
        efecto.color = new Color(color.r, color.g, color.b, alphaMax);

        float tiempo = 0f;
        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            float alpha = Mathf.Lerp(alphaMax, 0, t);
            efecto.color = new Color(color.r, color.g, color.b, alpha);
            tiempo += Time.deltaTime;
            yield return null;
        }

        efecto.gameObject.SetActive(false);
    }
}