using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOro : MonoBehaviour
{
    public float velocidadRotacion = 180f;

    private Vector3 ejeRotacion;

    void Start()
    {
        int eje = Random.Range(0, 3);

        if (eje == 0)
            ejeRotacion = Vector3.right;
        else if (eje == 1)
            ejeRotacion = Vector3.up;
        else
            ejeRotacion = Vector3.forward;
    }

    void Update()
    {
        transform.Rotate(ejeRotacion * velocidadRotacion * Time.deltaTime);
    }

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