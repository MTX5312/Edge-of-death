using System.Collections;
using UnityEngine;

public class AltarBarqueroManager : MonoBehaviour
{
    public DemonicAltar_Controller altarController;
    public GameObject barquero;
    public GameObject dialogoUI;
    public GameObject ticketPrefab;
    public Transform ticketSpawnPoint;
    public float tiempoDialogo = 4f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(SecuenciaBarquero());
        }
    }

    IEnumerator SecuenciaBarquero()
    {
        // Activa los efectos del altar
        altarController.ToggleDemonicAltar();

        // Espera un momento para hacerlo más dramático
        yield return new WaitForSeconds(1f);

        // Hace aparecer al barquero
        barquero.SetActive(true);

        // Muestra el diálogo
        dialogoUI.SetActive(true);

        yield return new WaitForSeconds(tiempoDialogo);

        dialogoUI.SetActive(false);

        // Crea el ticket flotante
        Instantiate(ticketPrefab, ticketSpawnPoint.position, Quaternion.identity);

        // (Más adelante: el jugador lo recoge -> se agrega al inventario)
    }
}
