using UnityEngine;

public class ZonaDetector : MonoBehaviour
{
    public SlowMoOnDash slowMo;
    private string zonaActual = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pereza")
        {
            slowMo.estaEnPereza = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Pereza")
        {
            slowMo.estaEnPereza = false;

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}