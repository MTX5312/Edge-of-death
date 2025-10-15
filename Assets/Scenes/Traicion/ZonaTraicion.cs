using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaInversion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScriptJugador player = other.GetComponent<ScriptJugador>();
            if (player != null)
            {
                player.EnterInversionZone();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScriptJugador player = other.GetComponent<ScriptJugador>();
            if (player != null)
            {
                player.ExitInversionZone();
            }
        }
    }
}