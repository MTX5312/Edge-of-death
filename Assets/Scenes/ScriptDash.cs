using System.Collections;
using UnityEngine;

public class ScriptDash : MonoBehaviour
{
    public float dashDistancia = 3f;
    public float dashTiempo = 0.2f;
    public float dashCooldown = 1f;
    private float ultimoDash;

    public Transform Body;

    private bool dashing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > ultimoDash + dashCooldown && !dashing)
        {
            // Leer la dirección de movimiento
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector3 movimiento = new Vector3(x, 0, y).normalized;

            // Si no hay input, dash hacia adelante
            Vector3 direccion = (movimiento.sqrMagnitude > 0.01f)
                ? Body.TransformDirection(movimiento)
                : Body.forward;

            StartCoroutine(Dash(direccion));
        }
    }

    IEnumerator Dash(Vector3 direccion)
    {
        dashing = true;
        ultimoDash = Time.time;

        Vector3 inicio = transform.position;
        Vector3 destino = inicio + direccion * dashDistancia;

        float tiempo = 0f;
        while (tiempo < dashTiempo)
        {
            transform.position = Vector3.Lerp(inicio, destino, tiempo / dashTiempo);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = destino;
        dashing = false;
    }
}

