using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDash : MonoBehaviour
{
    public float dashDistancia = 3f; //metros que avanza
    public float dashTiempo = 0.2f; //duración del dash
    public float dashCooldown = 1f; //tiempo entre dashes
    private float ultimoDash;

    public Transform Body; //asigna el mismo Body del jugador

    private bool dashing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > ultimoDash + dashCooldown && !dashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        dashing = true;
        ultimoDash = Time.time;

        Vector3 inicio = transform.position;
        Vector3 destino = inicio + Body.forward * dashDistancia;

        float tiempo = 0f;
        while (tiempo < dashTiempo)
        {
            transform.position = Vector3.Lerp(inicio, destino, tiempo);
            tiempo += Time.deltaTime;
            yield return null;
        }
    }
}