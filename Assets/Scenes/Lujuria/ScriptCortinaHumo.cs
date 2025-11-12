using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScriptCortinaHumo : MonoBehaviour
{
    public enum ChangeMode { Toggle, SetTrue, SetFalse }

    [Header("Detección del jugador")]
    public string playerTag = "Player";

    [Header("Qué hacer al recibir señal")]
    public ChangeMode onSignal = ChangeMode.Toggle;

    [Header("Destinos (asignar por Inspector)")]
    public List<ScriptCortinaHumo> targets = new List<ScriptCortinaHumo>();

    [Header("Collider opcional (si está en un hijo)")]
    public Collider customCollider;

    private Collider _col;

    void Awake()
    {
        _col = customCollider != null ? customCollider : GetComponent<Collider>();

        if (_col != null && !_col.isTrigger)
        {
            _col.isTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        foreach (var t in targets)
        {
            if (t == null) continue;
            t.ReceiveSignal();
        }

        Destroy(gameObject);
    }

    public void ReceiveSignal()
    {
        if (_col == null)
        {
            return;
        }

        switch (onSignal)
        {
            case ChangeMode.Toggle:
                _col.isTrigger = !_col.isTrigger;
                break;
            case ChangeMode.SetTrue:
                _col.isTrigger = true;
                break;
            case ChangeMode.SetFalse:
                _col.isTrigger = false;
                break;
        }
    }
}