using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeCode : MonoBehaviour
{
    public Shader gridShader;
    public Color gridColor = Color.gray;
    public float gridSize = 5f;
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

            if (rend == null) continue;

            if (gridEnabled)
            {
                Material[] newMats = new Material[rend.sharedMaterials.Length];
                for (int i = 0; i < rend.sharedMaterials.Length; i++)
                {
                    Material original = rend.sharedMaterials[i];
                    Material gridMat = new Material(gridShader);

                    if (original.HasProperty("_MainTex"))
                    {
                        Texture tex = original.GetTexture("_MainTex");
                        if (tex != null)
                            gridMat.SetTexture("_MainTex", tex);
                    }

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
                rend.materials = kvp.Value;
            }
        }
    }
}