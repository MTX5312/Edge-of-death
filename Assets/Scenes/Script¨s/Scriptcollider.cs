using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Scriptcollider : ScriptJugador
{
    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el player choca con un objeto llamado "Cube"
        if (collision.gameObject.name == "Cube")
        {
            Debug.Log("¡El jugador ha chocado con el cubo!");
        }
    }
}
