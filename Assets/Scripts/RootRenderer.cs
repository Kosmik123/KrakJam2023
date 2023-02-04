using System.Collections.Generic;
using UnityEngine;

public class RootRenderer : MonoBehaviour
{
    [SerializeField]
    private RootRendererSettings settings;
    [SerializeField]
    private Root root;
    [SerializeField]
    private MeshFilter meshFilter;

    private Mesh mesh;

    [ContextMenu("Generate mesh")]
    private void GenerateMesh()
    {
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        var nodes = root.Nodes;
        List<CombineInstance> meshInstances = new List<CombineInstance>(nodes.Count * 2);
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            Vector3Int nodePosition = node.Position;
            var nodeMatrix = Matrix4x4.TRS(node.Position, Quaternion.identity, 0.8f * Vector3.one);
            CombineInstance nodeMeshInstance = new CombineInstance()
            {
                mesh = settings.TurnMesh,
                transform = nodeMatrix,
            };
            meshInstances.Add(nodeMeshInstance);

            if (i > 0)
            {
                var previousNode = nodes[i - 1];
                Vector3Int previousNodePosition = previousNode.Position;
                Vector3 segmentPosition = ((Vector3)(previousNodePosition + nodePosition)) / 2f;
                int xDiff = previousNodePosition.x - nodePosition.x;
                int yDiff = previousNodePosition.y - nodePosition.y;
                int zDiff = previousNodePosition.z - nodePosition.z;
                SnapAxis segmentAxis = xDiff != 0 ? SnapAxis.X : zDiff != 0 ? SnapAxis.Z : SnapAxis.Y;
                Quaternion segmentRotation = segmentAxis == SnapAxis.X ? Quaternion.AngleAxis(90, Vector3.forward)
                    : segmentAxis == SnapAxis.Z ? Quaternion.AngleAxis(90, Vector3.right)
                    : Quaternion.identity;
                Vector3 segmentScale = new Vector3(1, Mathf.Abs(xDiff + yDiff + zDiff) - 1 , 1);

                var segmentMatrix = Matrix4x4.TRS(segmentPosition, segmentRotation, segmentScale);
                CombineInstance segmentMeshInstance = new CombineInstance()
                {
                    mesh = settings.StraightMesh,
                    transform = segmentMatrix,
                };
                meshInstances.Add(segmentMeshInstance);
            }
        }
        mesh.CombineMeshes(meshInstances.ToArray());
        meshFilter.sharedMesh = mesh;
    }






}
