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

    // Variables para la zona de Gula
    private float currentGravityMultiplier = 1f; // Multiplicador de gravedad (1 = normal)
    private float currentJumpReductionFactor = 1f; // Reducción de salto (1 = normal)
    private float currentSpeedReductionFactor = 1f; // Reducción de velocidad (1 = normal)
    private bool isInHighGravityZone = false; // Indica si el jugador está en la zona de alta gravedad
    
    // Variables para la zona de inversión
    private bool isInZonaTraicion = false; // Indica si el jugador está en la zona de traicion
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

        //Invertir controles si está en la zona de inversión
        if (isInZonaTraicion)
        {
            x = -x; // Invierte izquierda/derecha (A → derecha, D → izquierda)
            y = -y; // Invierte adelante/atrás (W → atrás, S → adelante)
            Debug.Log("Controles invertidos: x=" + x + ", y=" + y);
        }

        Movimiento();
        Deslizar();

        if (controller.isGrounded)
        {
            velocidadVertical = -1f;
            saltosRestantes = 2;
        }
        else
        {
            // Aplicar gravedad con multiplicador
            velocidadVertical += gravedad * currentGravityMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            // Aplicar reducción de salto si está en la zona de gula ( en zona traicion no se ve afectado)
            float effectiveJumpForce = (saltosRestantes == 2) ? fuerzaSalto * currentJumpReductionFactor : fuerzaDobleSalto * currentJumpReductionFactor;
            velocidadVertical = effectiveJumpForce;
            saltosRestantes--;
        }

        Vector3 movimiento = new Vector3(x, 0, y).normalized;

        if (movimiento.sqrMagnitude > 0.01f)
        {
            direccion = Body.TransformDirection(movimiento);

            // Aplicar reducción de velocidad so esta en zona gula
            Vector3 moveVector = direccion * (velocidadActual * currentSpeedReductionFactor) + Vector3.up * velocidadVertical;
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

        if (Input.GetKey(KeyCode.W) && controller.isGrounded)
        {
            if (velocidadActual < velocidadMaxima)
                velocidadActual += aceleracion * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && controller.isGrounded)
        {
            if (velocidadActual > velocidadMinima)
                velocidadActual -= freno * Time.deltaTime;
        }
        else if (controller.isGrounded)
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
            // Aplicar reducción de velocidad durante el deslizamiento si gula
            Vector3 moveVector = direccion * (velocidadActual * currentSpeedReductionFactor) + Vector3.up * velocidadVertical;
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

    // Métodos para la zona de inversión
    public void EnterInversionZone()
    {
        isInZonaTraicion = true;
    }

    public void ExitInversionZone()
    {
        isInZonaTraicion = false;
    }
}