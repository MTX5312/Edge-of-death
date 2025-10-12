using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOro : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            if (DeathZoneScript.currentRespawnPosition != Vector3.zero)
            {
                other.transform.position = DeathZoneScript.currentRespawnPosition;
            }

            ScriptJugador player = other.GetComponent<ScriptJugador>();
            if (player != null)
            {
                player.velocidadActual = 10f;
            }

            if (controller != null)
                controller.enabled = true;
        }
    }
}