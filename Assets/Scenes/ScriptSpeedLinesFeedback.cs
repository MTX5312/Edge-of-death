using UnityEngine;

public class ScriptSpeedLinesFeedback : MonoBehaviour
{
    public ScriptJugador jugador;

    public ParticleSystem lines50;
    public ParticleSystem lines100;

    public float umbral50 = 50f;
    public float umbral100 = 100f;

    private void Update()
    {
        float vel = jugador.velocidadActual;

        // 50
        if (vel >= umbral50)
        {
            if (!lines50.isPlaying) lines50.Play();
        }
        else
        {
            if (lines50.isPlaying) lines50.Stop();
        }

        // 100
        if (vel >= umbral100)
        {
            if (!lines100.isPlaying) lines100.Play();
        }
        else
        {
            if (lines100.isPlaying) lines100.Stop();
        }
    }
}
