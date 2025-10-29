using UnityEngine;

public class Ticket : MonoBehaviour
{
    [TextArea] public string pista = "Pista:\n- Usa salto doble\n- Desliza bajo trampas\n- Cuidado: gravedad x3";

  private void OnTriggerEnter(Collider other)
{
    Debug.Log($"COLISIÓN con: {other.name}, Tag: {other.tag}");

    if (other.CompareTag("Player"))
    {
        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        Debug.Log($"PlayerInventory encontrado: {(inv != null ? "SÍ" : "NO")}");

        if (inv != null)
        {
            inv.RecibirTicket(gameObject, pista);
            gameObject.SetActive(false);
            Debug.Log("TICKET DESACTIVADO");
        }
    }
}
}