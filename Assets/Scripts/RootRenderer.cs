using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootRenderer : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private Root root;
    [SerializeField]
    private Transform outsideMeshesHolder;    
    [SerializeField]
    private Transform insideMeshesHolder;

    [SerializeField]
    private Material outsideMaterial;
    [SerializeField]
    private Material insideMaterial;

    [Header("Settings")]
    [SerializeField]
    private RootRendererSettings settings;
    [SerializeField]
    private int maxSingleSegmentLength;

    private Mesh mesh;

    [ContextMenu("Clear")]
    public void Clear()
    {
        var meshRenderers = outsideMeshesHolder.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in meshRenderers)
            BetterDestroy(renderer.gameObject);

        meshRenderers = insideMeshesHolder.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in meshRenderers)
            BetterDestroy(renderer.gameObject);
    }

    public static void BetterDestroy(GameObject gameObject)
    {
        if (Application.isPlaying)
        {
            Destroy(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject); 
        }
    }

    [ContextMenu("Generate Renderers")]
    public void GenerateRenderers()
    {
        Clear();

        var nodes = root.Nodes;
        int nodesCount = nodes.Count;
        if (nodesCount == 0)
            return;

        Vector3Int previousNodePosition = nodes[0].Position;
        for (int nodeIndex = 1; nodeIndex < nodesCount; nodeIndex++)
        {
            bool isFirstNode = nodeIndex == 1;
            bool isLastNode = nodeIndex == nodesCount - 1;

            var node = nodes[nodeIndex];
            Vector3Int nodePosition = node.Position;

            int xDiff = nodePosition.x - previousNodePosition.x;
            int yDiff = nodePosition.y - previousNodePosition.y;
            int zDiff = nodePosition.z - previousNodePosition.z;

            Vector3 direction = new Vector3(xDiff, yDiff, zDiff).normalized;
            int signedLength = xDiff + yDiff + zDiff;
            int length = Mathf.Abs(signedLength);

            int singleSegmentsCount = (length - 1) / maxSingleSegmentLength + 1;
            float singleSegmentLength = length / (float)singleSegmentsCount;
            Quaternion segmentRotation = direction.x != 0 ? Quaternion.AngleAxis(90, Vector3.forward)
                : direction.z != 0 ? Quaternion.AngleAxis(90, Vector3.right)
                : Quaternion.identity;

            if (isFirstNode && (zDiff < 0 || yDiff < 0 || xDiff != 0))
            {
                Vector3 firstNodeRotationAxis = GetOtherAxis(direction);
                segmentRotation *= Quaternion.AngleAxis( 180, firstNodeRotationAxis);
                if (xDiff > 0)
                    segmentRotation *= Quaternion.AngleAxis(180, Vector3.right);
            }
            else if (isLastNode && (zDiff > 0 || yDiff > 0 || xDiff < 0))
            {
                Vector3 lastNodeRotationAxis = GetOtherAxis(direction);
                segmentRotation = Quaternion.AngleAxis(180, lastNodeRotationAxis) * segmentRotation;
            }

            Vector3 firstSingleSegmentOffeset = 0.5f * singleSegmentLength * direction;
            for (int segmentIndex = 0; segmentIndex < singleSegmentsCount; segmentIndex++)
            {
                Mesh segmentMesh = (isFirstNode && segmentIndex == 0) || (nodeIndex == nodesCount - 1 && segmentIndex == singleSegmentsCount - 1)
                    ? settings.EndingMesh : settings.StraightMesh;

                Vector3 segmentPosition = previousNodePosition + firstSingleSegmentOffeset + segmentIndex * singleSegmentLength * direction;
                CreateMeshRenderer(segmentMesh, segmentPosition, segmentRotation, singleSegmentLength, outsideMeshesHolder, outsideMaterial);
            }


            previousNodePosition = nodePosition;
        }
    }

    private void CreateMeshRenderer(Mesh mesh, Vector3 position, Quaternion rotation, float length, Transform parent, Material material) 
    {
        var gameObject = new GameObject($"Mesh {parent.transform.childCount}");
        var transform = gameObject.transform;
        transform.localPosition = position;
        transform.localScale = new Vector3(1, length, 1);
        transform.localRotation = rotation;
        transform.parent = parent;
        
        var filter = gameObject.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;
        var renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        if (Application.isPlaying)
        {
            filter.mesh = mesh;
            renderer.material = material;
        }
    }

    //[ContextMenu("Generate mesh")]
    public void GenerateMesh()
    {
        var nodes = root.Nodes;
        int nodesCount = nodes.Count;
        if (nodesCount == 0)
            return;

        List<CombineInstance> meshInstances = new List<CombineInstance>(nodesCount * 2);
        Vector3Int previousNodePosition = nodes[0].Position;
        for (int nodeIndex = 1; nodeIndex < nodesCount; nodeIndex++)
        {
            bool isFirstNode = nodeIndex == 1;
            bool isLastNode = nodeIndex == nodesCount - 1;

            var node = nodes[nodeIndex];
            Vector3Int nodePosition = node.Position;

            int xDiff = nodePosition.x - previousNodePosition.x;
            int yDiff = nodePosition.y - previousNodePosition.y;
            int zDiff = nodePosition.z - previousNodePosition.z;

            Vector3 direction = new Vector3(xDiff, yDiff, zDiff).normalized;
            int signedLength = xDiff + yDiff + zDiff;
            int length = Mathf.Abs(signedLength);

            int singleSegmentsCount = (length - 1) / maxSingleSegmentLength + 1;
            float singleSegmentLength = length / (float)singleSegmentsCount;
            Quaternion segmentRotation = direction.x != 0 ? Quaternion.AngleAxis(90, Vector3.forward)
                : direction.z != 0 ? Quaternion.AngleAxis(90, Vector3.right)
                : Quaternion.identity;

            if (isFirstNode && signedLength < 0)
            {
                Vector3 firstNodeRotationAxis = GetOtherAxis(direction);
                segmentRotation *= Quaternion.AngleAxis(180, firstNodeRotationAxis);
            }
            else if (isLastNode && (zDiff > 0 || yDiff > 0 || xDiff < 0))
            {
                Vector3 lastNodeRotationAxis = GetOtherAxis(direction);
                segmentRotation = Quaternion.AngleAxis(180, lastNodeRotationAxis) * segmentRotation;
            }

            Vector3 segmentScale = new Vector3(1, singleSegmentLength, 1);
            Vector3 firstSingleSegmentOffeset = 0.5f * singleSegmentLength * direction; 

            for (int segmentIndex = 0; segmentIndex < singleSegmentsCount; segmentIndex++)
            {
                Mesh segmentMesh = (isFirstNode && segmentIndex == 0) || (nodeIndex == nodesCount - 1 && segmentIndex == singleSegmentsCount - 1)
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
            mesh = new Mesh();
            previousNodePosition = nodePosition;
        }
    }

    private static Vector3 GetOtherAxis(Vector3 direction)
    {
        return direction.z != 0 ? Vector3.right : direction.x != 0 ? Vector3.up : Vector3.forward;
    }
}
