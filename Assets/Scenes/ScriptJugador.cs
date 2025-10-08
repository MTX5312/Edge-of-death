using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadActual = 20f;
    private Vector3 direccion;

    [Header("Deslizar")]
    public float tiempoRestanteSlide = 0f;
    bool deslizando = false;

    [Header("Camara")]
    public ScriptCamara camara;

    [Header("PJ")]
    public Transform Body;
    public CapsuleCollider colJugador;
    public float alturaNormal = 2f;
    public float alturaSlide = 1f;

    [Header("Salto")]
    public float fuerzaSalto = 10f;
    public float fuerzaDobleSalto = 8f;
    public float gravedad = -15f;
    private float velocidadVertical;
    private int saltosRestantes = 2;

    // Nuevo: CharacterController
    private CharacterController controller;

    private void Start()
    {
        if (Body == null)
            Body = transform;

        controller = GetComponent<CharacterController>();

        if (colJugador == null)
            colJugador = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Movimiento();
        Deslizar();

        // --- Gravedad y salto ---
        if (controller.isGrounded)
        {
            velocidadVertical = -1f; // Para mantenerlo en el suelo
            saltosRestantes = 2;
        }
        else
        {
            velocidadVertical += gravedad * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            velocidadVertical = (saltosRestantes == 2) ? fuerzaSalto : fuerzaDobleSalto;
            saltosRestantes--;
        }

        // --- Movimiento ---
        Vector3 movimiento = new Vector3(x, 0, y).normalized;

        if (movimiento.sqrMagnitude > 0.01f)
        {
            direccion = Body.TransformDirection(movimiento);

            Vector3 moveVector = direccion * velocidadActual + Vector3.up * velocidadVertical;
            controller.Move(moveVector * Time.deltaTime);

            // Rotación con cámara
            if (camara != null && camara.camaraMovida)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 6f * Time.deltaTime);
            }
        }
        else
        {
            // Solo movimiento vertical si no hay input
            controller.Move(Vector3.up * velocidadVertical * Time.deltaTime);
        }
    }

    private void Movimiento()
    {
        float aceleracion = 5f;
        float velocidadMaxima = 100f;
        float velocidadMinima = 20f;
        float desaceleracion = 5f;
        float freno = 20f;

        if (Input.GetKey(KeyCode.W))
        {
            if (velocidadActual < velocidadMaxima)
                velocidadActual += aceleracion * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (velocidadActual > velocidadMinima)
                velocidadActual -= freno * Time.deltaTime;
        }
        else
        {
            if (velocidadActual > velocidadMinima)
                velocidadActual -= desaceleracion * Time.deltaTime;
        }
    }

    private void Deslizar()
    {
        float duracionSlide = 1f;
        float velocidadExtra = 10f;
        float cooldownSlide = 1f;
        float tiempoUltimoSlide = -10;

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) && !deslizando && Time.time > tiempoUltimoSlide + cooldownSlide)
        {
            deslizando = true;
            velocidadActual += velocidadExtra;
            direccion = transform.forward;
            tiempoRestanteSlide = duracionSlide;
            tiempoUltimoSlide = Time.time;
            CambiarAltura(alturaSlide);
        }

        if (deslizando)
        {
            tiempoRestanteSlide -= Time.deltaTime;
            Vector3 moveVector = direccion * velocidadActual + Vector3.up * velocidadVertical;
            controller.Move(moveVector * Time.deltaTime);

            velocidadActual = Mathf.MoveTowards(velocidadActual, 20f, (velocidadExtra / duracionSlide) * Time.deltaTime);

            if (tiempoRestanteSlide <= 0f)
            {
                deslizando = false;
                CambiarAltura(alturaNormal);
            }
        }
    }

    private void CambiarAltura(float nuevaAltura)
    {
        Vector3 escala = Body.localScale;
        escala.y = nuevaAltura;
        Body.localScale = escala;

        if (controller != null)
        {
            controller.height = nuevaAltura;
            controller.center = new Vector3(0, nuevaAltura / 2f, 0);
        }
    }
}