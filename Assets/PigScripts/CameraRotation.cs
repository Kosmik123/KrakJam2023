using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] Transform camera;

    private void Update()
    {
        operationCamera();
    }
    public void operationCamera()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            camera.rotation *= Quaternion.Euler(0, -90, 0);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            camera.rotation *= Quaternion.Euler(0, 90, 0);
        }
    }
}
