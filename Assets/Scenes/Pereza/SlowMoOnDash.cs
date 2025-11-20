using UnityEngine;

public class SlowMoOnDash : MonoBehaviour
{
    [Header("Tiempo")]
    public float slowTimeScale = 0.3f;
    public float normalTimeScale = 1f;
    public float dashTimeDuration = 2f;

    [Header("Estado")]
    public bool estaEnPereza = false;
    private bool dashOverride = false;
    private float timerDash = 0f;

    void Update()
    {
        if (estaEnPereza && !dashOverride)
        {
            Time.timeScale = slowTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        if (dashOverride)
        {
            timerDash -= Time.unscaledDeltaTime;
            if (timerDash <= 0f)
            {
                dashOverride = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ActivarDash();
        }
    }

    public void ActivarDash()
    {
        dashOverride = true;
        timerDash = dashTimeDuration;

        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}