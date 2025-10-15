using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaTraicion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScriptJugador Player = other.GetComponent<ScriptJugador>();
            if (Player != null)
            {
                Player.EnterInversionZone();
                Debug.Log("Entrando en ZonaInversion");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScriptJugador Player = other.GetComponent<ScriptJugador>();
            if (Player != null)
            {
                Player.ExitInversionZone();
                Debug.Log("Saliendo de ZonaInversion");
            }
        }
    }
}