using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Vector3 recorrido;
    public float velocidad = 3f;
    public float distanciaUmbral = 0.05f;

    private Vector3 puntoA;
    private Vector3 puntoB;
    private Vector3 destinoActual;

    void Start()
    {
        puntoA = transform.position;

        puntoB = puntoA + recorrido;

        destinoActual = puntoB;
    }

    void Update()
    {
        float paso = velocidad * Time.unscaledDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, paso);

        if (Vector3.Distance(transform.position, destinoActual) < distanciaUmbral)
        {
            destinoActual = (destinoActual == puntoA) ? puntoB : puntoA;
        }
    }
}