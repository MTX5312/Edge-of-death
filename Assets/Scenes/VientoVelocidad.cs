using UnityEngine;

public class VientoVelocidad : MonoBehaviour
{
    public Rigidbody jugadorRB;       // Rigidbody del jugador
    public ParticleSystem viento50;   // Efecto de viento cuando pasa 50
    public ParticleSystem viento100;  // Efecto de viento cuando pasa 100
    public Transform camaraJugador;   // Cámara del jugador

    [Header("Ajustes de posicionamiento")]
    public float distanciaFrontal = 2f; // Distancia delante de la cámara
    public float alturaOffset = 0f;     // Ajuste vertical si lo necesitás

    void Update()
    {
        float velocidadActual = jugadorRB.velocity.magnitude;

        // Control de activación
        if (velocidadActual >= 50f && velocidadActual < 100f)
        {
            if (!viento50.isPlaying) viento50.Play();
            if (viento100.isPlaying) viento100.Stop();
        }
        else if (velocidadActual >= 100f)
        {
            if (!viento100.isPlaying) viento100.Play();
            if (viento50.isPlaying) viento50.Stop();
        }
        else
        {
            if (viento50.isPlaying) viento50.Stop();
            if (viento100.isPlaying) viento100.Stop();
        }

        // Actualizamos la posición y rotación de las partículas delante de la cámara
        ActualizarPosicionViento(viento50);
        ActualizarPosicionViento(viento100);
    }

    void ActualizarPosicionViento(ParticleSystem viento)
    {
        if (viento == null) return;

        // Calculamos una posición delante de la cámara
        Vector3 frente = camaraJugador.forward;
        Vector3 posAdelante = camaraJugador.position + frente * distanciaFrontal + Vector3.up * alturaOffset;

        viento.transform.position = posAdelante;
        viento.transform.rotation = camaraJugador.rotation;
    }
}