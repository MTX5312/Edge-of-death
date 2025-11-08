using UnityEngine;
using TMPro;

public class TicketUI : MonoBehaviour
{
    public ScriptJugador player;
    public TMP_Text ticketText;

    void Start()
    {
        player.OnTicketChanged += UpdateTicketUI;
        UpdateTicketUI(player.ticket);
    }

    void UpdateTicketUI(int value)
    {
        ticketText.text = "X" + value;
    }
}
