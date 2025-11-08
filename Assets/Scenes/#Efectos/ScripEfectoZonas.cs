using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScripEfectoZonas : MonoBehaviour
{
    [Header("Efectos de Zona")]
    public Image traicionEffect;
    public Image gulaEffect;
    public Image avariciaEffect;
    public Image violenciaEffect;
    public Image lujuriaEffect;
    public Image herejiaEffect;
    public Image perezaEffect;
    public Image limboEffect;

    [Header("Configuración de Fade")]
    public float duracionFadeIn = 1f;
    public float duracionFadeOut = 0.5f;
    public float alphaMax = 1f;

    private Image efectoActual;
    private Coroutine fadeCoroutine;

    void Start()
    {
        OcultarTodosLosEfectos();
        ActivarLimbo();
    }

    public void MostrarEfecto(string nombreZona)
    {
        if (string.IsNullOrEmpty(nombreZona))
        {
            ActivarLimbo();
            return;
        }

        Image nuevoEfecto = null;

        switch (nombreZona)
        {
            case "Traicion": nuevoEfecto = traicionEffect; break;
            case "Gula": nuevoEfecto = gulaEffect; break;
            case "Avaricia": nuevoEfecto = avariciaEffect; break;
            case "Violencia": nuevoEfecto = violenciaEffect; break;
            case "Lujuria": nuevoEfecto = lujuriaEffect; break;
            case "Herejia": nuevoEfecto = herejiaEffect; break;
            case "Pereza": nuevoEfecto = perezaEffect; break;
        }

        if (nuevoEfecto == null)
        {
            ActivarLimbo();
            return;
        }

        if (efectoActual == nuevoEfecto && nuevoEfecto.gameObject.activeSelf)
            return;

        if (efectoActual != null && efectoActual != nuevoEfecto && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOut(efectoActual));
        }

        OcultarTodosLosEfectos();
        nuevoEfecto.gameObject.SetActive(true);
        efectoActual = nuevoEfecto;
        fadeCoroutine = StartCoroutine(FadeIn(nuevoEfecto));
    }

    public void OcultarTodosLosEfectos()
    {
        if (traicionEffect) traicionEffect.gameObject.SetActive(false);
        if (gulaEffect) gulaEffect.gameObject.SetActive(false);
        if (avariciaEffect) avariciaEffect.gameObject.SetActive(false);
        if (violenciaEffect) violenciaEffect.gameObject.SetActive(false);
        if (lujuriaEffect) lujuriaEffect.gameObject.SetActive(false);
        if (herejiaEffect) herejiaEffect.gameObject.SetActive(false);
        if (perezaEffect) perezaEffect.gameObject.SetActive(false);
        if (limboEffect) limboEffect.gameObject.SetActive(false);
    }

    private void ActivarLimbo()
    {
        if (efectoActual == limboEffect && limboEffect != null && limboEffect.gameObject.activeSelf)
            return;

        OcultarTodosLosEfectos();

        if (limboEffect != null)
        {
            limboEffect.gameObject.SetActive(true);
            efectoActual = limboEffect;

            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeIn(limboEffect));
        }
    }

    private IEnumerator FadeIn(Image img)
    {
        Color color = img.color;
        float tiempo = 0f;
        color.a = 0f;
        img.color = color;

        while (tiempo < duracionFadeIn)
        {
            float t = tiempo / duracionFadeIn;
            color.a = Mathf.Lerp(0, alphaMax, t);
            img.color = color;
            tiempo += Time.deltaTime;
            yield return null;
        }

        color.a = alphaMax;
        img.color = color;
    }

    private IEnumerator FadeOut(Image img)
    {
        Color color = img.color;
        float tiempo = 0f;
        float alphaInicial = color.a;

        while (tiempo < duracionFadeOut)
        {
            float t = tiempo / duracionFadeOut;
            color.a = Mathf.Lerp(alphaInicial, 0, t);
            img.color = color;
            tiempo += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        img.color = color;
        img.gameObject.SetActive(false);
    }
}