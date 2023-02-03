using UnityEngine;

public class TransformSmoother : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float updateSpeed;

    [Header("Properties")]
    [SerializeField]
    private Vector3 targetPosition;
    
    [SerializeField, EulerAngles]
    private Quaternion targetRotation;

    private float timer;
    public Vector3 Position
    {
        get => targetPosition;
        set
        {
            initialPositon = transform.position;
            targetPosition = value;
            timer = 0;
        }
    }

    public Quaternion Rotation
    {
        get => targetRotation;
        set
        {
            initialRotation = transform.rotation;
            targetRotation = value;
            timer = 0;
        }
    }

    private Vector3 currentPositon;
    private Vector3 initialPositon;
    
    private Quaternion currentRotation;
    private Quaternion initialRotation;

    private void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime * updateSpeed;
            currentPositon = Vector3.Lerp(initialPositon, targetPosition, timer);
            currentRotation = Quaternion.Lerp(initialRotation, targetRotation, timer);
        }
        else
        {
            currentPositon = targetPosition;
            currentRotation = targetRotation;
        }
        transform.SetPositionAndRotation(currentPositon, currentRotation);
    }

    private void OnValidate()
    {
        Position = targetPosition;
        Rotation = targetRotation;
    }

}
