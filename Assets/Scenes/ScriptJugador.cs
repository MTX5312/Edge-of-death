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

    private void Start()
    {

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
        
        Vector3 movimiento = new Vector3(x, 0, y).normalized * velocidadActual * Time.deltaTime;
        
        transform.Translate(movimiento, Space.World);
    }
}