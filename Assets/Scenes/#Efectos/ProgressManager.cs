using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    [Header("Referencia al Slider de Progreso")]
    public Slider progressBar;

    private int totalCheckpoints = 0;
    private int activatedIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(InitializeProgress());
    }

    private IEnumerator InitializeProgress()
    {
        yield return null;

        totalCheckpoints = CheckpointScript.AllCount;

        Debug.Log($"[ProgressManager] Total de checkpoints encontrados: {totalCheckpoints}");

        progressBar.value = 0;
    }

    public void SetActiveCheckpointIndex(int index)
    {
        activatedIndex = index;

        Debug.Log($"[ProgressManager] Activando checkpoint índice: {activatedIndex} / {totalCheckpoints - 1}");

        if (totalCheckpoints > 1)
            progressBar.value = (float)activatedIndex / (totalCheckpoints - 1);
        else
            progressBar.value = 1;
    }
}