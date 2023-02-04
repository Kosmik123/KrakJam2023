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

public class Root : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private int segmentsCount;
    public int SegmentsCount => segmentsCount;

    [Header("States")]
    [SerializeField]
    private List<RootNode> nodes = new List<RootNode>();
    public List<RootNode> Nodes { get => nodes; set => nodes = value; }

    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        int len = nodes.Count;
        if (len <= 0)
            return;

        Gizmos.color = Color.white;
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
