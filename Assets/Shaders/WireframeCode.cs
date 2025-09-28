using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class WireframeCode : MonoBehaviour
{
    public Shader gridShader;       // arrastra tu shader GridWorldOverlay aquí
    public Color gridColor = Color.green;
    public float gridSize = 1f;
    public float lineWidth = 0.02f;

    private Dictionary<MeshRenderer, Material[]> originalMaterials = new Dictionary<MeshRenderer, Material[]>();
    private bool gridEnabled = false;

    void Start()
    {
        if (gridShader == null)
        {
            Debug.LogError("GridManager: Asigna el shader GridWorldOverlay en el inspector.");
            return;
        }

        // Guardar todos los materiales originales
        MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();
        foreach (var rend in renderers)
        {
            originalMaterials[rend] = rend.sharedMaterials;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleGrid();
        }
    }

    void ToggleGrid()
    {
        gridEnabled = !gridEnabled;

        foreach (var kvp in originalMaterials)
        {
            MeshRenderer rend = kvp.Key;

            if (rend == null) continue; // por si algún objeto fue destruido

            if (gridEnabled)
            {
                Material[] newMats = new Material[rend.sharedMaterials.Length];
                for (int i = 0; i < rend.sharedMaterials.Length; i++)
                {
                    Material original = rend.sharedMaterials[i];
                    Material gridMat = new Material(gridShader);

                    // copiar textura base si existe
                    if (original.HasProperty("_MainTex"))
                    {
                        Texture tex = original.GetTexture("_MainTex");
                        if (tex != null)
                            gridMat.SetTexture("_MainTex", tex);
                    }

                    // aplicar parámetros de grilla
                    gridMat.SetColor("_GridColor", gridColor);
                    gridMat.SetFloat("_GridSize", gridSize);
                    gridMat.SetFloat("_LineWidth", lineWidth);
                    gridMat.SetFloat("_Enabled", 1);

                    newMats[i] = gridMat;
                }
                rend.materials = newMats;
            }
            else
            {
                // restaurar materiales originales
                rend.materials = kvp.Value;
            }
        }
    }
}