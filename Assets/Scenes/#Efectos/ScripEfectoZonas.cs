using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        OcultarTodosLosEfectos();
    }

    public void MostrarEfecto(string nombreZona)
    {
        OcultarTodosLosEfectos();

        switch (nombreZona)
        {
            case "Traicion":
                Activar(traicionEffect);
                break;
            case "Gula":
                Activar(gulaEffect);
                break;
            case "Avaricia":
                Activar(avariciaEffect);
                break;
            case "Violencia":
                Activar(violenciaEffect);
                break;
            case "Lujuria":
                Activar(lujuriaEffect);
                break;
            case "Herejia":
                Activar(herejiaEffect);
                break;
            case "Pereza":
                Activar(perezaEffect);
                break;
        }
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
    }

    private void Activar(Image img)
    {
        if (img != null)
            img.gameObject.SetActive(true);
    }
}