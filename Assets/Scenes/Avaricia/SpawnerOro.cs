using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOro : MonoBehaviour
{
    [Header("Prefab de la pieza de oro")]
    public GameObject oroPrefab;

    [Header("Área de generación (en unidades)")]
    public float spawnWidth = 10f;
    public float spawnDepth = 10f;

    [Header("Tiempos")]
    public float spawnInterval = 0.1f;

    public float destroyHeight = -5f;
    public int piecesPerSpawn = 6;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnGold), 0f, spawnInterval);
    }

    void SpawnGold()
    {
        if (oroPrefab == null) return;

        for (int i = 0; i < piecesPerSpawn; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnWidth / 2f, spawnWidth / 2f),
                0f,
                Random.Range(-spawnDepth / 2f, spawnDepth / 2f)
            );

            Vector3 spawnPos = transform.position + randomOffset;

            GameObject gold = Instantiate(oroPrefab, spawnPos, Quaternion.identity);
            gold.AddComponent<DestroyOnFall>().limitY = destroyHeight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnWidth, 0.1f, spawnDepth));
    }
}
