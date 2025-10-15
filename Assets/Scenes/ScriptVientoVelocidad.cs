using UnityEngine;

public class VientoVelocidad : MonoBehaviour
{
    public Rigidbody jugadorRB;       // Rigidbody del jugador
    public ParticleSystem viento50;   // Efecto de viento cuando pasa 50
    public ParticleSystem viento100;  // Efecto de viento cuando pasa 100

    void Update()
    {
        // Calculamos la velocidad actual (sin dirección)
        float velocidadActual = jugadorRB.velocity.magnitude;

        // Si está entre 50 y 99 → activar viento50
        if (velocidadActual >= 50f && velocidadActual < 100f)
        {
            if (!viento50.isPlaying)
                viento50.Play();
            if (viento100.isPlaying)
                viento100.Stop();
        }
        // Si alcanza o supera 100 → activar viento100
        else if (velocidadActual >= 100f)
        {
            if (!viento100.isPlaying)
                viento100.Play();
            if (viento50.isPlaying)
                viento50.Stop();
        }
        // Si baja de 50 → apagar ambos
        else
        {
            if (viento50.isPlaying)
                viento50.Stop();
            if (viento100.isPlaying)
                viento100.Stop();
        }
    }
}

