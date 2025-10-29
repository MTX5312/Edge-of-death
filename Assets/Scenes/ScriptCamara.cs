using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamara : MonoBehaviour
{
    public float sensibilidadBase = 150f;  // Base antes de ajuste
    public float sensibilidad = 150f;      // Ajustable desde UI
    public float mouseX, mouseY;

    public Transform Body;
    public Transform Head;

    public bool camaraMovida = false;

    [Header("Límites de mirada vertical")]
    public bool limitarMiradaVertical = true;   // ← Activado por defecto
    public float limiteSuperior = 80f;          // ← Parkour: ver cielo
    public float limiteInferior = -70f;         // ← Parkour: ver pies

    private float angleY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AjustarSensibilidadAPantalla();
    }

    void Update()
    {
        camaraMovida = false;

        // === Movimiento horizontal (sin cambios) ===
        float mouseXInput = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        if (Mathf.Abs(mouseXInput) > 0.001f)
        {
            Body.Rotate(Vector3.up, mouseXInput);
            camaraMovida = true;
        }

        // === Movimiento vertical (mejorado) ===
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

        // === DESBLOQUEAR MOUSE PARA UI (NUEVO) ===
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) // Clic derecho
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // === Ajuste automático según resolución ===
    void AjustarSensibilidadAPantalla()
    {
        float dpi = Screen.dpi;
        if (dpi <= 0) dpi = 96;

        float factorPantalla = Screen.width / 1920f;
        sensibilidad = sensibilidadBase * factorPantalla * (dpi / 96f);
    }

    // === Método público para UI ===
    public void SetSensibilidad(float valor)
    {
        sensibilidad = sensibilidadBase * valor;
    }
}