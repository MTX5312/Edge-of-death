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
    public float gravedad = -10f;
    private float velocidadVertical;
    private int saltosRestantes = 2;

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

        if (controller.isGrounded)
        {
            velocidadVertical = -1f;
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

        Vector3 movimiento = new Vector3(x, 0, y).normalized;

        if (movimiento.sqrMagnitude > 0.01f)
        {
            direccion = Body.TransformDirection(movimiento);

            Vector3 moveVector = direccion * velocidadActual + Vector3.up * velocidadVertical;
            controller.Move(moveVector * Time.deltaTime);

            if (camara != null && camara.camaraMovida)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 6f * Time.deltaTime);
            }
        }
        else
        {
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

    // Métodos para la zona de Gula
    public void EnterHighGravityZone(float gravityMultiplier, float jumpReductionFactor, float speedReductionFactor)
    {
        currentGravityMultiplier = gravityMultiplier;
        currentJumpReductionFactor = jumpReductionFactor;
        currentSpeedReductionFactor = speedReductionFactor;
        isInHighGravityZone = true;
    }

    public void ExitHighGravityZone()
    {
        currentGravityMultiplier = 1f;
        currentJumpReductionFactor = 1f;
        currentSpeedReductionFactor = 1f;
        isInHighGravityZone = false;
    }
}