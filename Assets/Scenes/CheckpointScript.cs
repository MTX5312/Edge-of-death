using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CheckpointScript : MonoBehaviour
{
    [Header("Referencias visuales")]
    public Renderer baseRenderer;
    public Color inactiveColor = Color.gray;
    public Color activeColor = Color.green;

    private bool isActive = false;

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

        if (baseRenderer != null)
            baseRenderer.material.color = inactiveColor;
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
            if (cp != null && cp.baseRenderer != null)
            {
                if (cp == this)
                {
                    cp.baseRenderer.material.color = activeColor;
                    cp.isActive = true;
                }
                else
                {
                    cp.baseRenderer.material.color = inactiveColor;
                    cp.isActive = false;
                }
            }
        }
    }
}