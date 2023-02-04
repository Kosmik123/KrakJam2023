using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootRenderer : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private Root root;
    [SerializeField]
    private MeshFilter outdideMeshFilter;    
    [SerializeField]
    private MeshFilter insideMeshFilter;

    [Header("Settings")]
    [SerializeField]
    private RootRendererSettings settings;
    [SerializeField]
    private int maxSingleSegmentLength;


    private Mesh mesh;


    [ContextMenu("Generate mesh")]
    private void GenerateMesh()
    {
        var nodes = root.Nodes;
        int nodesCount = nodes.Count;
        if (nodesCount == 0)
            return;

        SnapAxis previousSnapAxis;
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        List<CombineInstance> meshInstances = new List<CombineInstance>(nodesCount * 2);
        Vector3Int previousNodePosition = nodes[0].Position;

        for (int nodeIndex = 1; nodeIndex < nodesCount; nodeIndex++)
        {
            bool firstNode = nodeIndex == 1;
            bool lastNode = nodeIndex == nodesCount - 1;

            var node = nodes[nodeIndex];
            Vector3Int nodePosition = node.Position;

            int xDiff = nodePosition.x - previousNodePosition.x;
            int yDiff = nodePosition.y - previousNodePosition.y;
            int zDiff = nodePosition.z - previousNodePosition.z;

            Vector3 direction = new Vector3(xDiff, yDiff, zDiff).normalized;
            if (lastNode)
                Debug.Log("Last: " + direction);
            else if (firstNode)
                Debug.Log("First: " + direction);

            int signedLength = xDiff + yDiff + zDiff;
            int length = Mathf.Abs(signedLength);

            int singleSegmentsCount = (length - 1) / maxSingleSegmentLength + 1;
            float singleSegmentLength = length / (float)singleSegmentsCount;
            Quaternion segmentRotation = direction.x != 0 ? Quaternion.AngleAxis(90, Vector3.forward)
                : direction.z != 0 ? Quaternion.AngleAxis(90, Vector3.right)
                : Quaternion.identity;

            if (firstNode && signedLength < 0)
            {
                Vector3 firstNodeRotationAxis = GetOtherAxis(direction);
                segmentRotation *= Quaternion.AngleAxis(180, firstNodeRotationAxis);
            }
            else if (lastNode && (zDiff > 0 || yDiff > 0 || xDiff < 0))
            {
                Vector3 lastNodeRotationAxis = GetOtherAxis(direction);
                segmentRotation = Quaternion.AngleAxis(180, lastNodeRotationAxis) * segmentRotation;
            }

            Vector3 segmentScale = new Vector3(1, singleSegmentLength, 1);
            Vector3 firstSingleSegmentOffeset = 0.5f * singleSegmentLength * direction; 

            for (int segmentIndex = 0; segmentIndex < singleSegmentsCount; segmentIndex++)
            {
                Mesh segmentMesh = (firstNode && segmentIndex == 0) || (nodeIndex == nodesCount - 1 && segmentIndex == singleSegmentsCount - 1)
                    ? settings.EndingMesh : settings.StraightMesh;

                Vector3 segmentPosition = previousNodePosition + firstSingleSegmentOffeset + segmentIndex * singleSegmentLength * direction;
                var segmentMatrix = Matrix4x4.TRS(segmentPosition, segmentRotation, segmentScale);
                CombineInstance segmentMeshInstance = new CombineInstance()
                {
                    mesh = segmentMesh,
                    transform = segmentMatrix,
                };
                meshInstances.Add(segmentMeshInstance);
            }



            if (nodeIndex != nodesCount - 1)
            {
                var nodeMatrix = Matrix4x4.TRS(node.Position, Quaternion.identity, Vector3.one);
                CombineInstance nodeMeshInstance = new CombineInstance()
                {
                    mesh = settings.TurnMesh,
                    transform = nodeMatrix,
                };
                meshInstances.Add(nodeMeshInstance);
            }

            previousNodePosition = nodePosition;
        }

        mesh.CombineMeshes(meshInstances.ToArray());
        outdideMeshFilter.sharedMesh = mesh;
        var insideMesh = Instantiate(mesh);
        insideMesh.triangles = insideMesh.triangles.Reverse().ToArray();
        insideMeshFilter.sharedMesh = insideMesh;
    }

    private static Vector3 GetOtherAxis(Vector3 direction)
    {
        return direction.z != 0 ? Vector3.right : direction.x != 0 ? Vector3.up : Vector3.forward;
    }
}
