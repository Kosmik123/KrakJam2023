using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private GridPosition gridPosition;
    [SerializeField]
    private Transform forwardProvider;

    [Header("Input")]
    [SerializeField]
    private KeyCode upKey = KeyCode.W;
    [SerializeField]
    private KeyCode downKey = KeyCode.S;
    [SerializeField]
    private KeyCode leftKey = KeyCode.A;
    [SerializeField]
    private KeyCode rightKey = KeyCode.D;

    [Header("Settings")]
    [SerializeField]
    private LayerMask obstacleLayers;

    private void Reset()
    {
        gridPosition = GetComponent<GridPosition>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(upKey))
        {
            HandleMove(forwardProvider.up);
        }
        else if (Input.GetKeyDown(downKey))
        {
            HandleMove(-forwardProvider.up);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            HandleMove(forwardProvider.right);
        }
        else if (Input.GetKeyDown(leftKey))
        {
            HandleMove(-forwardProvider.right);
        }
    }

    private void HandleMove(Vector3 direction)
    {
        if (CanMove(direction))
            gridPosition.Position += direction.ToVector3Int();
    }

    private bool CanMove(Vector3 direction)
    {
        return !Physics.SphereCast(new Ray(transform.position, direction), 0.4f, 1.5f, obstacleLayers);
    }
}
