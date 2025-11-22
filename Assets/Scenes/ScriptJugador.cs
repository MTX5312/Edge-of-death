using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptJugador : MonoBehaviour
{
    // ----------------- TICKETS -----------------
    public int ticket = 0;
    public event Action<int> OnTicketChanged;

    public void AddTicket(int amount)
    {
        ticket += amount;
        OnTicketChanged?.Invoke(ticket);
    }

    // ----------------- MOVIMIENTO -----------------
    [Header("Movimiento")]
    public float velocidadActual = 20f;
    private Vector3 direccion;
    private Vector3 inputMovimiento; // ← Guarda el input ya invertido

    [Header("Deslizar")]
    public float tiempoRestanteSlide = 0f;
    bool deslizando = false;
    public Action OnSlide;
    private float alturaOriginalCollider;
    private Vector3 centroOriginalCollider;

    [Header("Camara")]
    public ScriptCamara camara;

    [Header("PJ")]
    public Transform Body;
    public CapsuleCollider colJugador;
    public float alturaNormal = 1f;
    public float alturaSlide = 1f;

    [Header("Salto")]
    public float fuerzaSalto = 10f;
    public float fuerzaDobleSalto = 8f;
    public float gravedad = -10f;
    private float velocidadVertical;
    private int saltosRestantes = 2;

    [Header("Frenado / Patinaje")]
    [Range(0f, 10f)] public float suavidadFrenado = 3f;
    public float velocidadUmbral = 0.1f;

    // ----------------- ZONAS ESPECIALES -----------------
    private float currentGravityMultiplier = 1f;
    private float currentJumpReductionFactor = 1f;
    private float currentSpeedReductionFactor = 1f;
    private bool isInHighGravityZone = false;

    public bool isInZonaTraicion = false;
    private CharacterController controller;

    private void Start()
    {
        if (Body == null)
            Body = transform;

        controller = GetComponent<CharacterController>();

        if (colJugador == null)
            colJugador = GetComponent<CapsuleCollider>();

        alturaOriginalCollider = controller.height;
        centroOriginalCollider = controller.center;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Aplicar inversión de controles si está en la zona de traición
        if (isInZonaTraicion)
        {
            x = -x;
            y = -y;
        }

        inputMovimiento = new Vector3(x, 0, y).normalized;

        Movimiento();
        Deslizar();

        // ----------------- SALTO & GRAVEDAD -----------------
        if (controller.isGrounded)
        {
            velocidadVertical = -1f;
            saltosRestantes = 2;
        }
        else
        {
            velocidadVertical += gravedad * currentGravityMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            float effectiveJumpForce = (saltosRestantes == 2)
                ? fuerzaSalto * currentJumpReductionFactor
                : fuerzaDobleSalto * currentJumpReductionFactor;

            velocidadVertical = effectiveJumpForce;
            saltosRestantes--;
        }

        // ----------------- MOVIMIENTO HORIZONTAL -----------------
        if (inputMovimiento.sqrMagnitude > 0.01f)
        {
            direccion = Body.TransformDirection(inputMovimiento);
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

    // ----------------- MOVIMIENTO ACELERACIÓN -----------------
    private void Movimiento()
    {
        float aceleracion = 3f;
        float velocidadMaxima = 100f;
        float velocidadMinima = 20f;
        float freno = 20f;

        if (velocidadActual >= 50f)
            aceleracion = 3f;
        else if (velocidadActual >= 80f)
            aceleracion = 3.5f;
        else
            aceleracion = 2.5f;

        bool moviendoAdelante = inputMovimiento.z > 0.1f;
        bool moviendoAtras = inputMovimiento.z < -0.1f;

        if (moviendoAdelante && controller.isGrounded)
        {
            if (velocidadActual < velocidadMaxima)
                velocidadActual += aceleracion * Time.deltaTime;
        }
        else if (moviendoAtras && controller.isGrounded)
        {
            if (velocidadActual > velocidadMinima)
                velocidadActual -= freno * Time.deltaTime;
        }
        else if (controller.isGrounded)
        {
            velocidadActual = Mathf.Lerp(velocidadActual, velocidadMinima, Time.deltaTime * suavidadFrenado);

            if (velocidadActual - velocidadMinima < velocidadUmbral)
                velocidadActual = velocidadMinima;
        }
    }

    // ----------------- DESLIZAR -----------------
    private void Deslizar()
    {
        float duracionSlide = 1f;
        float velocidadExtra = 6f;
        float cooldownSlide = 1f;
        float tiempoUltimoSlide = -10f;

        if (Input.GetKeyDown(KeyCode.LeftControl) && inputMovimiento.z > 0.1f && !deslizando && Time.time > tiempoUltimoSlide + cooldownSlide)
        {
            deslizando = true;
            OnSlide?.Invoke();
            velocidadActual += velocidadExtra;
            direccion = Body.TransformDirection(inputMovimiento);
            tiempoRestanteSlide = duracionSlide;
            tiempoUltimoSlide = Time.time;

            CambiarAltura(alturaSlide);
        }

        if (deslizando)
        {
            tiempoRestanteSlide -= Time.deltaTime;
            Vector3 moveVector = direccion * (velocidadActual * currentSpeedReductionFactor) + Vector3.up * velocidadVertical;
            controller.Move(moveVector * Time.deltaTime);

            velocidadActual = Mathf.MoveTowards(velocidadActual, 20f, (velocidadExtra / duracionSlide) * Time.deltaTime);

            if (tiempoRestanteSlide <= 0f)
            {
                deslizando = false;

                controller.height = alturaOriginalCollider;
                controller.center = centroOriginalCollider;
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

    // ----------------- ZONAS -----------------
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

    public void EnterInversionZone()
    {
        isInZonaTraicion = true;
    }

    public void ExitInversionZone()
    {
        isInZonaTraicion = false;
    }

    // ----------------- ENEMIGOS -----------------
    public void Bounce(float fuerza)
    {
        velocidadVertical = fuerza;
        saltosRestantes = 2;

        if (controller != null)
            controller.Move(Vector3.up * 0.1f);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            PenalizeAndRespawn();
        }
    }

    private void PenalizeAndRespawn()
    {
        controller.enabled = false;

        transform.position = DeathZoneScript.currentRespawnPosition;

        controller.enabled = true;

        if (EnemyManager.Instance != null)
            EnemyManager.Instance.ResetAllEnemies();
    }
}