using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZonaGravedad : MonoBehaviour
{
    public float gravityMultiplier = 2f; // Multiplicador de gravedad ( 2x para caída más rápida).
    public float jumpReductionFactor = 0.7f; // Reducción de la fuerza de salto ( 70% de la normal).
    public float speedReductionFactor = 0.6f; // Reducción de la velocidad ( 60% de la normal).

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScriptJugador player = other.GetComponent<ScriptJugador>();
            if (player != null)
            {
                player.EnterHighGravityZone(gravityMultiplier, jumpReductionFactor, speedReductionFactor);
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
                player.ExitHighGravityZone();
            }
        }
    }
}