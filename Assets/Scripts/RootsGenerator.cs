using System.Collections;
using UnityEngine;

public abstract class RootGenerationStrategy : MonoBehaviour
{
    public abstract void Generate(Root root);
}

public class RootsGenerator : MonoBehaviour
{
    public event System.Action OnRootsGenerated;

    [SerializeField]
    private Root[] roots;
    public Root[] Roots { get => roots; set => roots = value; }

    [SerializeField]
    private RootGenerationStrategy strategy;


    [ContextMenu("Generate")]
    public void Generate()
    {
        foreach (var root in roots)
        {
            strategy.Generate(root);
        }
        OnRootsGenerated?.Invoke();
    }
}
