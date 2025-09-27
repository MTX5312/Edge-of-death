using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridManager : MonoBehaviour
{
    public Shader gridShader;
    public Color gridColor = Color.green;
    public Color backgroundColor = Color.black;
    public float gridSize = 10f;
    public float lineWidth = 0.02f;
    public KeyCode toggleKey = KeyCode.F2;

    private bool gridEnabled = false;
    private Dictionary<MeshRenderer, Material[]> originalMaterials = new();
    private Material gridMaterial;

    void Start()
    {
        if (gridShader == null)
        {
            gridShader = Shader.Find("Unlit/GridToggle");
        }

        gridMaterial = new Material(gridShader);
        gridMaterial.SetColor("_GridColor", gridColor);
        gridMaterial.SetColor("_Background", backgroundColor);
        gridMaterial.SetFloat("_GridSize", gridSize);
        gridMaterial.SetFloat("_LineWidth", lineWidth);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            gridEnabled = !gridEnabled;
            if (gridEnabled) ApplyGrid();
            else RestoreOriginals();
        }
    }

    void ApplyGrid()
    {
        foreach (MeshRenderer renderer in FindObjectsOfType<MeshRenderer>())
        {
            if (!originalMaterials.ContainsKey(renderer))
            {
                originalMaterials[renderer] = renderer.sharedMaterials;
            }

            // reemplazar todos los materiales por el de grilla
            Material[] mats = new Material[renderer.sharedMaterials.Length];
            for (int i = 0; i < mats.Length; i++) mats[i] = gridMaterial;
            renderer.sharedMaterials = mats;
        }
    }

    void RestoreOriginals()
    {
        foreach (var kvp in originalMaterials)
        {
            if (kvp.Key != null)
            {
                kvp.Key.sharedMaterials = kvp.Value;
            }
        }
        originalMaterials.Clear();
    }
}