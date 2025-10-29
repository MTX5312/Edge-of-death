using UnityEngine;

public class ScriptObstaculo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró al trigger tiene el ScriptJugador
        ScriptJugador jugador = other.GetComponent<ScriptJugador>();

        if (jugador != null)
        {
            jugador.velocidadActual = 20f;
            Debug.Log("Jugador penalizado: velocidad reducida a 20");
        }
    }
}