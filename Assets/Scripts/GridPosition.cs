using UnityEngine;

public class GridPosition : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private TransformSmoother transformSmoother;

    [Header("States")]
    [SerializeField]
    private Vector3Int position;
    public Vector3Int Position
    {
        get => position;
        set
        {
            transformSmoother.Position = position = value;
        }
    }
}

public static class Vector3Extension
{
    public static Vector3Int ToVector3Int(this Vector3 vector)
    {
        return new Vector3Int(
            Mathf.RoundToInt(vector.x),
            Mathf.RoundToInt(vector.y),
            Mathf.RoundToInt(vector.z));
    }
}
