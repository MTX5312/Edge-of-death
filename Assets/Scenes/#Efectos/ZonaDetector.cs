using UnityEngine;

public class ZonaDetector : MonoBehaviour
{
    public ScripEfectoZonas efectoHUD;
    public SlowMoOnDash slowMo;

    public SkyboxManager skyboxManager;

    private string zonaActual = "";

    private void Start()
    {
        if (efectoHUD != null)
            efectoHUD.MostrarEfecto("");

        skyboxManager?.CambiarSkybox("");
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

                skyboxManager?.CambiarSkybox(zonaActual);

                if (other.gameObject.name == "Pereza")
                    slowMo.estaEnPereza = true;

                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == zonaActual)
        {
            zonaActual = "";
            efectoHUD.MostrarEfecto("");

            skyboxManager?.CambiarSkybox("");

            if (other.gameObject.name == "Pereza")
            {
                slowMo.estaEnPereza = false;
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
            }
        }
    }
}