using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RootNode
{
    [field: SerializeField]
    public Vector3Int Position { get; private set; }

    public RootNode(Vector3Int position)
    {
        Position = position;
    }
}

public class RootGenerable : MonoBehaviour
{
    private readonly Vector3Int[] directions =
    {
        Vector3Int.up,
        Vector3Int.left,
        Vector3Int.forward,
        Vector3Int.down,
        Vector3Int.right,
        Vector3Int.back
    };

    [Header("Settings")]
    //[SerializeField]
    //private int minLength;
    //[SerializeField]
    //private int maxLength;
    [SerializeField]
    private int segmentsCount;
    [Space]
    [SerializeField]
    private int minSegmentLength;
    [SerializeField]
    private int maxSegmentLength;

    [Header("States")]
    [SerializeField]
    private List<RootNode> nodes = new List<RootNode>();

    [ContextMenu("Generate")]
    private void Generate()
    {
        nodes.Clear();
        var node = new RootNode(Vector3Int.zero);
        nodes.Add(node);
        var direction = GetRandomDirection();
        for (int i = 0; i < segmentsCount; i++)
        {
            var length = GetSegmentLength();
            node = new RootNode(node.Position + direction * length);
            nodes.Add(node);
            direction = GetRandomDirection(direction);
        }
    }    

    private int GetSegmentLength()
    {
        return Random.Range(minSegmentLength, maxSegmentLength);
    }

    private Vector3Int GetRandomDirection()
    {
        return directions[Random.Range(0, directions.Length)];
    }

    private Vector3Int GetRandomDirection(Vector3Int exception)
    {
        int index = Random.Range(0, directions.Length);
        if (directions[index] == exception)
            index = (index + 1) % directions.Length;
        return directions[index];
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        int len = nodes.Count;
        if (len <= 0)
            return;

        GizmosDrawNode(nodes[0], position);
        for (int i = 1; i < len; i++)
        {
            Gizmos.DrawLine(nodes[i - 1].Position, nodes[i].Position);
            GizmosDrawNode(nodes[i], position);
        }
    }

    private void GizmosDrawNode(RootNode node, Vector3 position)
    {
        const float nodeRadius = 0.1f;
        //Gizmos.DrawSphere(position.ToVector3Int() + node.Position, nodeRadius);
    }
}
