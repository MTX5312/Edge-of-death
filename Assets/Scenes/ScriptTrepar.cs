using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ScriptTrepar : MonoBehaviour
{
    [Header("Configuración de detección")]
    public float distanciaDeteccion = 1.0f;
    public float alturaDeteccion = 1.5f;
    public LayerMask capaBorde;

    [Header("Configuración de escalada")]
    public float alturaSubida = 1.0f;
    public float offsetAdelante = 0.4f;

    private ScriptJugador jugador;
    private CharacterController controller;

    private bool colgado = false;
    private Vector3 posicionBorde;
    private Vector3 posicionColgado;

    private void Start()
    {
        jugador = GetComponent<ScriptJugador>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!colgado && !controller.isGrounded)
        {
            VerificarBorde();
        }

        if (colgado)
        {
            controller.enabled = false;
            transform.position = posicionColgado;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SubirInstantaneamente();
            }
        }
    }

    private void VerificarBorde()
    {
        Vector3 origen = transform.position + Vector3.up * alturaDeteccion;

        if (Physics.Raycast(origen, transform.forward, out RaycastHit hit, distanciaDeteccion))
        {
            if (hit.collider.CompareTag("Borde"))
            {
                posicionBorde = hit.point;

                posicionColgado = posicionBorde
                    - transform.forward * 0.35f
                    - Vector3.up * 0.4f;

                colgado = true;

                jugador.enabled = false;
                controller.Move(Vector3.zero);
                controller.enabled = false;
            }
        }
    }

    private void SubirInstantaneamente()
    {
        Vector3 posicionFinal = posicionBorde
            + Vector3.up * alturaSubida
            + transform.forward * offsetAdelante;

        transform.position = posicionFinal;

        controller.enabled = true;
        jugador.enabled = true;
        colgado = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 origen = transform.position + Vector3.up * alturaDeteccion;
        Gizmos.DrawLine(origen, origen + transform.forward * distanciaDeteccion);
    }
}