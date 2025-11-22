using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum MoveAxis { Horizontal, Vertical }
    public MoveAxis axis = MoveAxis.Horizontal;

    public float speed = 2f;
    public Vector3 pointA;
    public Vector3 pointB;
    private Vector3 origin;
    private Vector3 target;
    private Rigidbody rb;

    void Awake()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        //  usa un rango por defecto
        if (pointA == Vector3.zero && pointB == Vector3.zero)
        {
            if (axis == MoveAxis.Horizontal)
            {
                pointA = origin + Vector3.left * 2f;
                pointB = origin + Vector3.right * 2f;
            }
            else
            {
                pointA = origin + Vector3.up * 1f;
                pointB = origin + Vector3.down * 1f;
            }
        }

        // empezamos yendo a pointB
        target = pointB;
    }

    void Update()
    {
        // Movimiento simple hacia target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    // Guarda la posición de origen para poder resetear
    public void ResetToOrigin()
    {
        transform.position = origin;
        target = pointB;
    }
}
