using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CheckpointScript : MonoBehaviour
{
    [Header("Referencias visuales")]
    public Renderer baseRenderer;
    public Renderer cylinderRenderer;
    public Color baseInactiveColor = Color.gray;
    public Color baseActiveColor = Color.green;
    public Material lightInactiveColor;
    public Material lightActiveColor;

    private bool isActive = false;

    private static List<CheckpointScript> allCheckpoints = new List<CheckpointScript>();
    public static int AllCount => allCheckpoints.Count;

    private void Awake()
    {
        if (!allCheckpoints.Contains(this))
            allCheckpoints.Add(this);
    }

    private void OnDestroy()
    {
        allCheckpoints.Remove(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (baseRenderer == null)
            baseRenderer = GetComponentInParent<Renderer>();

        if (cylinderRenderer == null)
            cylinderRenderer = GetComponentInChildren<Renderer>();

        if (!isActive)
        {
            if (baseRenderer != null)
                baseRenderer.material.color = baseInactiveColor;

            if (cylinderRenderer != null)
                cylinderRenderer.material = lightInactiveColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeathZoneScript.currentRespawnPosition = transform.position;
            ActivateCheckpoint();
            Debug.Log("Nuevo checkpoint activado: " + name);
        }
    }

    private void ActivateCheckpoint()
    {
        foreach (CheckpointScript cp in allCheckpoints)
        {
            if (cp == null) continue;

            if (cp == this)
            {
                cp.baseRenderer.material.color = baseActiveColor;
                cp.cylinderRenderer.material = lightActiveColor;
                cp.isActive = true;
            }
            else
            {
                cp.baseRenderer.material.color = baseInactiveColor;
                cp.cylinderRenderer.material = lightInactiveColor;
                cp.isActive = false;
            }
        }

        int index = allCheckpoints.IndexOf(this);
        ProgressManager.Instance.SetActiveCheckpointIndex(index);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Vector3 currentRespawn = DeathZoneScript.currentRespawnPosition;
        bool respawnIsFromThisScene = false;

        foreach (CheckpointScript cp in allCheckpoints)
        {
            if (cp != null && cp.gameObject.scene == SceneManager.GetActiveScene())
            {
                if (cp.transform.position == currentRespawn)
                {
                    respawnIsFromThisScene = true;
                    break;
                }
            }
        }

        if (!respawnIsFromThisScene)
        {
            GameObject respawn = GameObject.Find("RespawnPoint");

            if (respawn != null)
            {
                DeathZoneScript.currentRespawnPosition = respawn.transform.position;
                Debug.Log("RespawnPoint reasignado porque el checkpoint era de otra escena.");
            }
            else
            {
                Debug.LogWarning("No se encontró un objeto llamado 'RespawnPoint' en la escena: " + scene.name);
            }
        }
    }
}