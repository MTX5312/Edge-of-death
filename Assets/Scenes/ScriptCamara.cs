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
    public bool limitarMiradaVertical = false;  // ← Opción para activar/desactivar
    public float limiteSuperior = 90f;
    public float limiteInferior = -90f;

    private float angleY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AjustarSensibilidadAPantalla(); // ← Ajuste automático
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
            // Si NO limitamos → permite 360° vertical (opcional)

            Head.localRotation = Quaternion.Euler(angleY, 0, 0);
            camaraMovida = true;
        }
    }

    // === Ajuste automático según resolución ===
    void AjustarSensibilidadAPantalla()
    {
        float dpi = Screen.dpi;
        if (dpi <= 0) dpi = 96; // Valor por defecto

        // Ajuste proporcional: pantallas grandes → más sensibilidad
        float factorPantalla = Screen.width / 1920f;
        sensibilidad = sensibilidadBase * factorPantalla * (dpi / 96f);
    }

    // === Método público para UI ===
    public void SetSensibilidad(float valor)
    {
        sensibilidad = sensibilidadBase * valor;
    }
}