using System.Collections;
using UnityEngine;

public abstract class RootGenerationStrategy : MonoBehaviour
{
    public abstract void Generate(Root root);
}

public class RootGenerator : MonoBehaviour
{
    [SerializeField]
    private Root[] roots;
    [SerializeField]
    private RootGenerationStrategy strategy;

    [ContextMenu("Generate")]
    private void Generate()
    {
        foreach (var root in roots)
        {
            strategy.Generate(root);
        }
    }
}
