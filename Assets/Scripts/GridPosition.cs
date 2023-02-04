using UnityEngine;

public class GridPosition : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float updateSpeed;

    [Header("States")]
    [SerializeField]
    private Vector3Int position;
    public Vector3Int Position
    {
        get => position;
        set
        {
            position = value;
            timer = 0;
        }
    }

    private float timer;

    private void Update()
    {
        if (timer > 1)
        {
            transform.position = position;
        }
        else
        {
            timer += updateSpeed * Time.deltaTime;

        }


    }


}


