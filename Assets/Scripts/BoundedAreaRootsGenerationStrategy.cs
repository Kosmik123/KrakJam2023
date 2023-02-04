using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedAreaRootsGenerationStrategy : RootGenerationStrategy
{
    [Header("Settings")]
    [SerializeField]
    private BoundsInt generationArea;

    private int axis;

    public override void Generate(Root root)
    {
        var nodes = root.Nodes;
        nodes.Clear();
        axis = Random.Range(0, 3);

        Vector3Int target = new Vector3Int(
            Random.Range(generationArea.xMin, generationArea.xMax),
            Random.Range(generationArea.yMin, generationArea.yMax),
            Random.Range(generationArea.zMin, generationArea.zMax));
        nodes.Add(new RootNode(target));

        for (int i = 0; i < root.SegmentsCount; i++)
        {
            axis += Random.Range(0, 2) + 1;
            axis %= 3;
            switch (axis)
            {
                case 0:
                    target.x = Random.Range(generationArea.xMin, generationArea.xMax);
                    break;
                    
                case 1:
                    target.y = Random.Range(generationArea.yMin, generationArea.yMax);
                    break;
                    
                case 2:
                    target.z = Random.Range(generationArea.zMin, generationArea.zMax);
                    break;
            }
            nodes.Add(new RootNode(target));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(generationArea.center, generationArea.size);
    }
}
