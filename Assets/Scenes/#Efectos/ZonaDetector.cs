using UnityEngine;

public class ZonaDetector : MonoBehaviour
{
    public ScripEfectoZonas efectoHUD;
    public string zonaActual { get; private set; } = "";

    private void Start()
    {
        if (efectoHUD != null)
            efectoHUD.MostrarEfecto("");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (efectoHUD == null) return;

        switch (other.gameObject.name)
        {
            case "Traicion":
            case "Gula":
            case "Avaricia":
            case "Violencia":
            case "Lujuria":
            case "Herejia":
            case "Pereza":
                zonaActual = other.gameObject.name;
                efectoHUD.MostrarEfecto(zonaActual);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (efectoHUD == null) return;

        if (other.gameObject.name == zonaActual)
        {
            zonaActual = "";
            efectoHUD.MostrarEfecto("");
        }
    }
}