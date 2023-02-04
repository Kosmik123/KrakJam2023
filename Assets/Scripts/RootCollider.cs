using UnityEngine;

public class RootCollider : MonoBehaviour
{
    [SerializeField]
    private Root root;

    [ContextMenu("Generate Colliders")]
    public void GenerateColliders()
    {
        var nodes = root.Nodes;
        int count = nodes.Count;
        for (int i = 1; i < count; i++)
        {
            var firstNode = nodes[i - 1];
            var secondNode = nodes[i];

            Vector3 segmentPosition = ((Vector3)firstNode.Position + (Vector3)secondNode.Position) / 2f;
            int xDiff = secondNode.Position.x - firstNode.Position.x;
            int yDiff = secondNode.Position.y - firstNode.Position.y;
            int zDiff = secondNode.Position.z - firstNode.Position.z;

            Vector3 direction = new Vector3(xDiff, yDiff, zDiff).normalized;
            int signedLength = xDiff + yDiff + zDiff;
            int length = Mathf.Abs(signedLength);

            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = segmentPosition;
            capsuleCollider.direction = zDiff != 0 ? 2 : yDiff != 0 ? 1 : 0;
            capsuleCollider.radius = 0.4f;
            capsuleCollider.height = length;
        }
    }
}
