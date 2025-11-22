using UnityEngine;

public class ScriptSpeedLinesFeedback : MonoBehaviour
{
    [Header("Jugador y Velocidad")]
    public ScriptJugador jugador;

    [Header("Speed Lines")]
    public ParticleSystem lines50;
    public ParticleSystem lines100;

    public float umbral50 = 50f;
    public float umbral100 = 100f;

    [Header("Dash Lines")]
    public ScriptDash dashScript;          // referencia al script del dash
    public ParticleSystem dashLines;       // partícula del dash
    public float dashLinesDuration = 0.25f;

    private void Start()
    {
        // Suscribirse al evento del dash
        if (dashScript != null)
            dashScript.OnDash += PlayDashLines;
    }

    private void OnDestroy()
    {
        // Evitar fugas de memoria si se destruye el objeto
        if (dashScript != null)
            dashScript.OnDash -= PlayDashLines;
    }

    private void Update()
    {
        float vel = jugador.velocidadActual;

        // --- SpeedLines 50 ---
        if (vel >= umbral50)
        {
            if (!lines50.isPlaying) lines50.Play();
        }
        else
        {
            if (lines50.isPlaying) lines50.Stop();
        }

        // --- SpeedLines 100 ---
        if (vel >= umbral100)
        {
            if (!lines100.isPlaying) lines100.Play();
        }
        else
        {
            if (lines100.isPlaying) lines100.Stop();
        }
    }


    
    private void PlayDashLines()
    {
        if (dashLines == null) return;

        dashLines.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        dashLines.Play();

        // detener después
        Invoke(nameof(StopDashLines), dashLinesDuration);
    }

    private void StopDashLines()
    {
        if (dashLines != null)
            dashLines.Stop();
    }
}

