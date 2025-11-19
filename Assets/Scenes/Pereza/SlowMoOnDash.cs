using UnityEngine;
using System.Collections;

public class SlowMoOnDash : MonoBehaviour
{
    public ScriptDash dashScript;
    public ZonaDetector zonaDetector;

    [Header("Slow Motion Settings")]
    public float slowTimeScale = 0.25f;
    public float dashOverrideDuration = 1.5f;
    public float enterSpeed = 6f;
    public float exitSpeed = 3f;

    private bool overrideActive = false;

    void Start()
    {
        dashScript.OnDash += HandleDash;
    }

    void Update()
    {
        float targetTimeScale;

        if (zonaDetector.zonaActual == "Pereza" && !overrideActive)
        {
            targetTimeScale = slowTimeScale;
        }
        else
        {
            targetTimeScale = 1f;
        }

        Time.timeScale = Mathf.Lerp(
            Time.timeScale,
            targetTimeScale,
            Time.unscaledDeltaTime * (targetTimeScale < Time.timeScale ? enterSpeed : exitSpeed)
        );

        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void HandleDash()
    {
        if (zonaDetector.zonaActual != "Pereza") return;

        StartCoroutine(DashOverrideRoutine());
    }

    IEnumerator DashOverrideRoutine()
    {
        overrideActive = true;
        yield return new WaitForSecondsRealtime(dashOverrideDuration);
        overrideActive = false;
    }
}