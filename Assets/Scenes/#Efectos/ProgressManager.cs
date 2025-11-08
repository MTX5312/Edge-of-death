using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    [Header("Referencia al Slider de Progreso")]
    public Slider progressBar;

    private int totalCheckpoints;
    private int activatedIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Asigna automáticamente la cantidad total al iniciar
        totalCheckpoints = CheckpointScript.AllCount;
        progressBar.value = 0;
    }

    public void SetTotalCheckpoints(int amount)
    {
        totalCheckpoints = amount;
    }

    public void SetActiveCheckpointIndex(int index)
    {
        activatedIndex = index;

        if (totalCheckpoints > 1)
            progressBar.value = (float)activatedIndex / (totalCheckpoints - 1);
        else
            progressBar.value = 1;
    }
}