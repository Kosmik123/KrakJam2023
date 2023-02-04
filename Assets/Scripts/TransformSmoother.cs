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
            initialPositon = transform.localPosition;
            targetPosition = value;
            timer = 0;
        }
    }

    public Quaternion Rotation
    {
        get => targetRotation;
        set
        {
            initialRotation = transform.localRotation;
            targetRotation = value;
            timer = 0;
        }
    }

    private Vector3 currentPositon;
    private Vector3 initialPositon;
    
    private Quaternion currentRotation;
    private Quaternion initialRotation;

    private void Awake()
    {
        currentRotation = targetRotation = transform.localRotation;
        currentPositon = targetPosition = transform.localPosition;
    }

    private void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime * updateSpeed;
            currentPositon = Vector3.Lerp(initialPositon, targetPosition, timer);
            currentRotation = new Quaternion(
                Mathf.Lerp(initialRotation.x, targetRotation.x, timer),
                Mathf.Lerp(initialRotation.y, targetRotation.y, timer),
                Mathf.Lerp(initialRotation.z, targetRotation.z, timer),
                Mathf.Lerp(initialRotation.w, targetRotation.w, timer));
        }
        else
        {
            currentPositon = targetPosition;
            currentRotation = targetRotation;
        }
        transform.localPosition = currentPositon;
        transform.localRotation = currentRotation;
    }

    private void OnValidate()
    {
        Position = targetPosition;
        Rotation = targetRotation;
    }

}
