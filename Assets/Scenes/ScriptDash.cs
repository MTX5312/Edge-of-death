using System.Collections;
using UnityEngine;

public class ScriptDash : MonoBehaviour
{
    [Header("Dash")]
    public float dashDistancia = 3f;
    public float dashTiempo = 0.2f;
    public float dashCooldown = 1f;
    private float ultimoDash;

    [Header("Referencias")]
    public Transform Body;
    public ScriptJugador jugador;
    public System.Action OnDash;

    private bool dashing = false;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (jugador == null)
            jugador = GetComponent<ScriptJugador>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > ultimoDash + dashCooldown && !dashing)
        {
            OnDash?.Invoke();
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (jugador != null && jugador.isInZonaTraicion)
            {
                x = -x;
                y = -y;
            }

            Vector3 movimiento = new Vector3(x, 0, y).normalized;

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

        float dashVelocidad = dashDistancia / dashTiempo;

        float tiempo = 0f;
        while (tiempo < dashTiempo)
        {
            controller.Move(direccion * dashVelocidad * Time.deltaTime);
            tiempo += Time.deltaTime;
            yield return null;
        }

        dashing = false;
    }
}