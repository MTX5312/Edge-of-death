using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class ScriptJugador : MonoBehaviour
{
    public float velocidadActual = 20f;
    public float tiempoRestanteSlide = 0f;
    private Vector3 direccion;
    bool deslizando = false;

    public Transform Body;
    public ScriptCamara camara;

    private void Start()
    {
        if (Body == null)
        {
            Body = transform; // Usa el propio jugador como referencia
        }
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Caminar();
        Deslizar();
        
        Vector3 movimiento = new Vector3(x, 0, y).normalized;
        
        //Acomodar camara
        if (movimiento.sqrMagnitude > 0.01f)
        {
            direccion = Body.TransformDirection(movimiento);

            transform.Translate(direccion * velocidadActual * Time.deltaTime, Space.World);

            if (camara != null && camara.camaraMovida)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rotacionObjetivo,
                    6f * Time.deltaTime
                );
            }
        }
    }

    void Caminar()
    {
        float aceleracion = 5f;
        float velocidadMaxima = 100f;
        float velocidadMinima = 20f;
        float desaceleracion = 5f;
        float freno = 20f;
        if (Input.GetKey(KeyCode.W))
        {
            if (velocidadActual < velocidadMaxima)
            {
                velocidadActual += aceleracion * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (velocidadActual > velocidadMinima)
            {
                velocidadActual -= freno * Time.deltaTime;
            }    
        }
        else
        {
            if (velocidadActual > velocidadMinima)
            {
                velocidadActual -= desaceleracion * Time.deltaTime;
            }
        }
    }

    void Deslizar()
    {
        float duracionSlide = 2f;
        float velocidadExtra = 20f;
        float cooldownSlide = 1.5f;
        float tiempoUltimoSlide = -10;

        if (Input.GetKeyDown(KeyCode.LeftControl) && !deslizando && Time.time > tiempoUltimoSlide + cooldownSlide)
        {
            deslizando = true;
            velocidadActual += velocidadExtra;
            direccion = transform.forward;
            tiempoRestanteSlide = duracionSlide;
            tiempoUltimoSlide = Time.time;
        }
        if (deslizando)
        {
            tiempoRestanteSlide -= Time.deltaTime;
            transform.Translate(direccion * velocidadActual * Time.deltaTime, Space.World);
            velocidadActual = Mathf.MoveTowards(velocidadActual, 20f, (velocidadExtra / duracionSlide) * Time.deltaTime);

            if (tiempoRestanteSlide <= 0f)
            {
                deslizando = false;
            }
        }
    }
}