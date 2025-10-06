using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                // Deshabilitamos el CharacterController para poder teletransportar sin problemas
                controller.enabled = false;
                other.transform.position = respawnPoint.position;
                controller.enabled = true;
            }
            else
            {
                // Por si acaso el objeto tiene Rigidbody en vez de CharacterController
                other.transform.position = respawnPoint.position;

                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

            // Reiniciamos parámetros del jugador
            ScriptJugador playerScript = other.GetComponent<ScriptJugador>();
            if (playerScript != null)
            {
                playerScript.velocidadActual = 10f;
            }
        }
    }
}