using UnityEngine;

public class TicketPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScriptJugador player = other.GetComponent<ScriptJugador>();

        if (player != null)
        {
            player.AddTicket(1);
            Destroy(gameObject);
        }
    }
}