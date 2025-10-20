using System.Collections;
using UnityEngine;

public class ScriptDash : MonoBehaviour
{
    [Header("Dash")]
    public float dashDistancia = 3f;       // Distancia total del dash
    public float dashTiempo = 0.2f;        // Tiempo que dura el dash
    public float dashCooldown = 1f;        // Tiempo entre dashes
    private float ultimoDash;

    [Header("Referencias")]
    public Transform Body;
    public ScriptJugador jugador;          // ← Referencia al script del jugador

    private bool dashing = false;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Si no se asigna desde el inspector, lo buscamos en el mismo objeto
        if (jugador == null)
            jugador = GetComponent<ScriptJugador>();
    }

    void Update()
    {
        // No hacer dash si ya está dashing o en cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > ultimoDash + dashCooldown && !dashing)
        {
            // Leer la dirección de movimiento
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            // Aplicar inversión de controles si está en la zona de traición
            if (jugador != null && jugador.isInZonaTraicion)
            {
                x = -x;
                y = -y;
            }

            Vector3 movimiento = new Vector3(x, 0, y).normalized;

            // Si no hay input, dash hacia adelante
            Vector3 direccion = (movimiento.sqrMagnitude > 0.01f)
                ? Body.TransformDirection(movimiento)
                : Body.forward;

            StartCoroutine(Dash(direccion));
        }
    }

    IEnumerator Dash(Vector3 direccion)
    {
        dashing = true;
        ultimoDash = Time.time;

        // Velocidad del dash (distancia / tiempo)
        float dashVelocidad = dashDistancia / dashTiempo;

        float tiempo = 0f;
        while (tiempo < dashTiempo)
        {
            controller.Move(direccion * dashVelocidad * Time.deltaTime);
            tiempo += Time.deltaTime;
            yield return null;
        }

        dashing = false;
    }
}