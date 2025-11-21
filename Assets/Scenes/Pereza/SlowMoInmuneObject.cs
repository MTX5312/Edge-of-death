using UnityEngine;

public class SlowMoInmuneObject : MonoBehaviour
{
    private SlowMoOnDash slowMo;
    private PlataformaMovil plataforma;

    void Start()
    {
        slowMo = FindObjectOfType<SlowMoOnDash>();
        plataforma = GetComponent<PlataformaMovil>();
    }

    void Update()
    {
        if (plataforma == null) return;

        plataforma.enabled = !(slowMo != null && slowMo.DashActivo);
    }
}