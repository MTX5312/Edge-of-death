using UnityEngine;
using TMPro;

public class TicketUI : MonoBehaviour
{
    public ScriptJugador player;
    public TMP_Text ticketText;

    void Start()
    {
        // Nos suscribimos al evento
        player.OnTicketChanged += UpdateTicketUI;

        // Actualizar al inicio
        UpdateTicketUI(player.ticket);
    }

    private void OnDestroy()
    {
        // Buenas prácticas: desuscribir evento
        player.OnTicketChanged -= UpdateTicketUI;
    }

    void UpdateTicketUI(int value)
    {
        ticketText.text = "X" + value;
    }
}