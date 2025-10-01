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
    public float velocidadCambioAltura = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;   // altura del salto
    public float gravedad = -20f;    // fuerza de gravedad
    private float velocidadVertical; // velocidad en eje Y
    private bool enSuelo = true;     // si está apoyado



    private void Start()
    {
        if (Body == null)
            Body = transform;

        if (colJugador == null)
            colJugador = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Movimiento();
        Deslizar();

        Vector3 movimiento = new Vector3(x, 0, y).normalized;

        if (movimiento.sqrMagnitude > 0.01f)
        {
            direccion = Body.TransformDirection(movimiento);
            transform.Translate(direccion * velocidadActual * Time.deltaTime, Space.World);

            if (camara != null && camara.camaraMovida)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 6f * Time.deltaTime);
            }
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
            transform.Translate(direccion * velocidadActual * Time.deltaTime, Space.World);
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
        // Cambiar la escala del cuerpo (solo eje Y para altura)
        Vector3 escala = Body.localScale;
        escala.y = nuevaAltura;
        Body.localScale = escala;

        if (colJugador != null)
        {
            colJugador.height = nuevaAltura;
            colJugador.center = new Vector3(colJugador.center.x, nuevaAltura / 2f, colJugador.center.z);
        }
    }
}