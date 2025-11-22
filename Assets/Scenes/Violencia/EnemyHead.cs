using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Enemy parentEnemy;
    public float bounceForce = 12f;

    private void Awake()
    {
        if (parentEnemy == null)
            parentEnemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var jugador = other.GetComponent<ScriptJugador>();
            if (jugador != null)
            {
                jugador.Bounce(bounceForce);
            }
        }
    }
}
