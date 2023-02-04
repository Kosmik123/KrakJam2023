using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] TransformSmoother transformSmoother;
    [SerializeField] bool UpAndDown;
    private void Update()
    {
        operationCamera();
    }

    public void operationCamera()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            transformSmoother.Rotation *= Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transformSmoother.Rotation *= Quaternion.Euler(0, 90, 0);
        }
        if (UpAndDown)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                transformSmoother.Rotation *= Quaternion.Euler(90, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transformSmoother.Rotation *= Quaternion.Euler(-90,0, 0);
            }
        }
    }

}
