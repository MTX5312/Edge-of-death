using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptJugador : MonoBehaviour
{
    public float aceleracion = 5f;
    public float velocidadMaxima = 100f;
    public float velocidadMinima = 20f;
    public float velocidadActual = 20f;
    public float desaceleracion = 5f;
    public float freno = 10f;

    public ScriptCamara camara;
    public float sensibilidad = 6f;

    public Transform Body;

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
        if (Input.GetKey(KeyCode.W))
        {
            velocidadActual += aceleracion * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocidadActual -= freno * Time.deltaTime;
        }
        else
        {
            velocidadActual -= desaceleracion * Time.deltaTime;
        }
        velocidadActual = Mathf.Clamp(velocidadActual, velocidadMinima, velocidadMaxima);

        Vector3 movimiento = new Vector3(x, 0, y).normalized;

        if (movimiento.sqrMagnitude > 0.01f)
        {
            Vector3 direccion = Body.TransformDirection(movimiento);

            transform.Translate(direccion * velocidadActual * Time.deltaTime, Space.World);

            if (camara != null && camara.camaraMovida)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rotacionObjetivo,
                    sensibilidad * Time.deltaTime
                );
            }
        }
    }
}