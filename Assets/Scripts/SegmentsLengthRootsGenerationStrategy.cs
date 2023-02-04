using UnityEngine;

public class SegmentsLengthRootsGenerationStrategy : RootGenerationStrategy
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

    [SerializeField]
    private int minSegmentLength;
    [SerializeField]
    private int maxSegmentLength;

    public override void Generate(Root root)
    {
        int segmentsCount = root.SegmentsCount;
        var nodes = root.Nodes;
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
}
