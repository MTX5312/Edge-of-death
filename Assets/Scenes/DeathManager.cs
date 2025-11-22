using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject deathScreen;  // Arrastra el prefab DeathScreen aquí

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowDeathScreen();
        }
    }

    public void ShowDeathScreen()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
    }

    // Botón Reaparecer
    public void Reaparecer()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        deathScreen.SetActive(false);

        // Usa exactamente el mismo código que ya tenía tu compañero
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = DeathZoneScript.currentRespawnPosition;

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) { cc.enabled = false; cc.enabled = true; }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null) { rb.velocity = Vector3.zero; rb.angularVelocity = Vector3.zero; }

        ScriptJugador sj = player.GetComponent<ScriptJugador>();
        if (sj != null)
        {
            sj.ExitHighGravityZone();
            sj.velocidadActual = 20f;
        }
    }

    // Botón Volver al Menú
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
