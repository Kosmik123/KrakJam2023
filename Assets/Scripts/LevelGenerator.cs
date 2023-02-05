using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public event System.Action OnLevelGenerated;

    [SerializeField]
    private RootsGenerator rootsGenerator;

    [ContextMenu("Generate level")]
    public void GenerateLevel()
    {
        var roots = rootsGenerator.GetComponentsInChildren<Root>();
        rootsGenerator.Roots = roots;
        rootsGenerator.OnRootsGenerated += RootsGenerator_OnRootsGenerated;
        rootsGenerator.Generate();
    }

    private void RootsGenerator_OnRootsGenerated()
    {
        rootsGenerator.OnRootsGenerated -= RootsGenerator_OnRootsGenerated;
        var roots = rootsGenerator.Roots;
        foreach (var root in roots)
        {
            var rootRenderer = root.GetComponent<RootRenderer>();
            rootRenderer.GenerateRenderers();
            
            var rootCollider = root.GetComponent<RootCollider>();
            rootCollider.GenerateColliders();
        }

        OnLevelGenerated?.Invoke();
    }
}
