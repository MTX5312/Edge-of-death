using UnityEngine;

public class SlowMoImmuneObject : MonoBehaviour
{
    public float speed = 5f;
    public float slowedSpeed = 2f;

    private SlowMoOnDash slowMo;

    void Start()
    {
        slowMo = FindObjectOfType<SlowMoOnDash>();
    }

    void Update()
    {
        float currentSpeed;

        if (slowMo.estaEnPereza && !slowMoIsDashOverride())
            currentSpeed = speed;
        else
            currentSpeed = slowedSpeed;

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    bool slowMoIsDashOverride()
    {
        return Time.timeScale >= 0.99f;
    }
}