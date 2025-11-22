using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [Header("Skybox por Zona")]
    public Material skyboxTraicion;
    public Material skyboxGula;
    public Material skyboxAvaricia;
    public Material skyboxViolencia;
    public Material skyboxLujuria;
    public Material skyboxHerejia;
    public Material skyboxPereza;
    public Material skyboxLimbo;

    [Header("Skybox por Defecto")]
    public Material skyboxDefault;

    public void CambiarSkybox(string zona)
    {
        Material mat = null;

        switch (zona)
        {
            case "Traicion": mat = skyboxTraicion; break;
            case "Gula": mat = skyboxGula; break;
            case "Avaricia": mat = skyboxAvaricia; break;
            case "Violencia": mat = skyboxViolencia; break;
            case "Lujuria": mat = skyboxLujuria; break;
            case "Herejia": mat = skyboxHerejia; break;
            case "Pereza": mat = skyboxPereza; break;

            default:
                mat = skyboxDefault;
                break;
        }

        if (mat != null)
        {
            RenderSettings.skybox = mat;
            DynamicGI.UpdateEnvironment();
        }
    }

    private void Start()
    {
        if (skyboxDefault != null)
        {
            RenderSettings.skybox = skyboxLimbo;
            DynamicGI.UpdateEnvironment();
        }
    }
}