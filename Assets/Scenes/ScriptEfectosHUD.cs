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
    public float duracionDash = 0.8f;
    public float duracionSlide = 1f;
    public float alphaMaxDash = 1f;
    public float alphaMaxSlide = 2f;
    public Color colorDash = Color.white;
    public Color colorSlide = Color.cyan;

    [Header("Efectos de sonido")]
    public AudioSource audioSource; // Si no se asigna, se creará automáticamente
    public AudioClip sonidoDash;    // Sonido para el dash
    public AudioClip sonidoSlide;   // Sonido para el slide

    private void Awake()
    {
        // Crear AudioSource si no está asignado
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Evitar que reproduzca al iniciar
        }
    }

    private void Start()
    {
        // Desactivar imágenes al inicio
        if (dashEffect != null)
            dashEffect.gameObject.SetActive(false);
        if (slideEffect != null)
            slideEffect.gameObject.SetActive(false);

        // Suscribirse a eventos
        if (dash != null)
            dash.OnDash += MostrarEfectoDash;

        if (jugador != null)
            jugador.OnSlide += MostrarEfectoSlide;
    }

    private void OnDestroy()
    {
        // Desuscribirse
        if (dash != null)
            dash.OnDash -= MostrarEfectoDash;

        if (jugador != null)
            jugador.OnSlide -= MostrarEfectoSlide;
    }

    private void MostrarEfectoDash()
    {
        if (dashEffect != null)
            StartCoroutine(EfectoVisual(dashEffect, colorDash, alphaMaxDash, duracionDash));

        if (audioSource != null && sonidoDash != null)
            audioSource.PlayOneShot(sonidoDash);
    }

    private void MostrarEfectoSlide()
    {
        if (slideEffect != null)
            StartCoroutine(EfectoVisual(slideEffect, colorSlide, alphaMaxSlide, duracionSlide));

        if (audioSource != null && sonidoSlide != null)
            audioSource.PlayOneShot(sonidoSlide);
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