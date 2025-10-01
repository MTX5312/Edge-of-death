using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObstaculo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Esto chequea si el objeto que chocó tiene el ScriptJugador
        ScriptJugador jugador = collision.gameObject.GetComponent<ScriptJugador>();
        if (jugador != null)

        {
            //Penaliza y reduce la velocidad a 20
            jugador.velocidadActual = 20f;
        }
    }
}