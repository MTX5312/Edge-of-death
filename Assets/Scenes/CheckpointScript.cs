using System.Collections;
using UnityEngine;
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
    public static int AllCount => allCheckpoints.Count;
    private static List<CheckpointScript> allCheckpoints = new List<CheckpointScript>();

    private void Awake()
    {
        allCheckpoints.Add(this);
    }

    private void OnDestroy()
    {
        allCheckpoints.Remove(this);
    }

    private void Start()
    {
        if (baseRenderer == null)
        {
            baseRenderer = GetComponentInParent<Renderer>();
        }
        if (cylinderRenderer == null)
        {
            cylinderRenderer = GetComponentInChildren<Renderer>();
        }

        if (baseRenderer != null)
            baseRenderer.material.color = baseInactiveColor;
        if (cylinderRenderer != null)
            cylinderRenderer.material = lightInactiveColor;
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
            if (cp != null && cp.baseRenderer != null && cp.cylinderRenderer != null)
            {
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
    }
}