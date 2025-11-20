using UnityEngine;

public class ZonaDetector : MonoBehaviour
{
    public ScripEfectoZonas efectoHUD;
    public SlowMoOnDash slowMo;

    private string zonaActual = "";

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

                if (other.gameObject.name == "Pereza")
                {
                    slowMo.estaEnPereza = true;
                }
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

            if (other.gameObject.name == "Pereza")
            {
                slowMo.estaEnPereza = false;

                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
            }
        }
    }
}