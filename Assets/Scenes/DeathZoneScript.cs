using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public static Vector3 currentRespawnPosition;
    public Transform defaultRespawnPoint;

    private void Start()
    {
        if (currentRespawnPosition == Vector3.zero && defaultRespawnPoint != null)
        {
            currentRespawnPosition = defaultRespawnPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
           
            // Desactivar CharacterController para mover al jugador
            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = currentRespawnPosition;
                controller.enabled = true;
            }
            else
            {
                other.transform.position = currentRespawnPosition;

                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

            ScriptJugador playerScript = other.GetComponent<ScriptJugador>();
           // Restablecer efectos de la zona de Gula y velocidad
            if (playerScript != null)
            {
                playerScript.ExitHighGravityZone(); // Resetea gravedad, salto y velocidad
                playerScript.velocidadActual = 20f; // Valor por defecto de ScriptJugador.cs
            }

            Debug.Log("Jugador respawneado en: " + currentRespawnPosition);
        }
    }
}
