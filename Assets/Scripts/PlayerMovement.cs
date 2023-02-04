using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GridPosition gridPosition;
    [SerializeField]
    private Transform forwardProvider;

    [SerializeField]
    private KeyCode upKey = KeyCode.W;
    [SerializeField]
    private KeyCode downKey = KeyCode.S;
    [SerializeField]
    private KeyCode leftKey = KeyCode.A;
    [SerializeField]
    private KeyCode rightKey = KeyCode.D;

    private void Reset()
    {
        gridPosition = GetComponent<GridPosition>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            gridPosition.Position += forwardProvider.up.ToVector3Int();
        }
        else if (Input.GetKeyDown(downKey))
        {
            gridPosition.Position -= forwardProvider.up.ToVector3Int();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            gridPosition.Position += forwardProvider.right.ToVector3Int();
        }
        else if (Input.GetKeyDown(leftKey))
        {
            gridPosition.Position -= forwardProvider.right.ToVector3Int();
        }
    }
}
