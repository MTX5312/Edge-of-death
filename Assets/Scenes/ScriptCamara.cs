using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamara : MonoBehaviour
{
    public float sensibilidadBase = 150f;
    public float sensibilidad = 150f;
    public float mouseX, mouseY;

    public Transform Body;
    public Transform Head;

    public bool camaraMovida = false;

    [Header("Efecto de velocidad (FOV)")]
    public Camera camara;
    public ScriptJugador jugador;
    public float fovBase = 45f;
    public float fovMax = 90f;
    public float velocidadMax = 100f;
    public float suavizadoFOV = 5f;

    [Header("Límites de mirada vertical")]
    public bool limitarMiradaVertical = true;
    public float limiteSuperior = 80f;
    public float limiteInferior = -70f;

    private float angleY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AjustarSensibilidadAPantalla();
    }

    void Update()
    {
        camaraMovida = false;

        float mouseXInput = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        if (Mathf.Abs(mouseXInput) > 0.001f)
        {
            Body.Rotate(Vector3.up, mouseXInput);
            camaraMovida = true;
        }

        float mouseYInput = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        if (Mathf.Abs(mouseYInput) > 0.001f)
        {
            angleY -= mouseYInput;

            if (limitarMiradaVertical)
            {
                angleY = Mathf.Clamp(angleY, limiteInferior, limiteSuperior);
            }

            Head.localRotation = Quaternion.Euler(angleY, 0, 0);
            camaraMovida = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (jugador != null && camara != null)
        {
            float velocidad = jugador.velocidadActual;
            float t = Mathf.InverseLerp(0, velocidadMax, velocidad);
            float fovObjetivo = Mathf.Lerp(fovBase, fovMax, t);
            camara.fieldOfView = Mathf.Lerp(camara.fieldOfView, fovObjetivo, Time.deltaTime * suavizadoFOV);
        }
    }
    void AjustarSensibilidadAPantalla()
    {
        float dpi = Screen.dpi;
        if (dpi <= 0) dpi = 96;

        float factorPantalla = Screen.width / 1920f;
        sensibilidad = sensibilidadBase * factorPantalla * (dpi / 96f);
    }
    public void SetSensibilidad(float valor)
    {
        sensibilidad = sensibilidadBase * valor;
    }
}
