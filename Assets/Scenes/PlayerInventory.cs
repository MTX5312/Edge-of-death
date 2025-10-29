using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public GameObject ticket;
    public GameObject panelPista;
    public TextMeshProUGUI textoPista;

    public void RecibirTicket(GameObject t, string pista)
    {
        ticket = t;
        textoPista.text = pista;
        panelPista.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && ticket != null)
        {
            panelPista.SetActive(!panelPista.activeSelf);
        }
    }

    public bool Sellar()
    {
        if (ticket != null)
        {
            Destroy(ticket);
            ticket = null;
            panelPista.SetActive(false);
            return true;
        }
        return false;
    }
}