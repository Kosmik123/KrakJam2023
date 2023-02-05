using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceController : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera rootsCamera;

    [Header("Settings")]
    [SerializeField]
    private float cameraDistance;

    private void OnValidate()
    {
        var mainCameraLocalPosition = mainCamera.transform.localPosition;
        mainCameraLocalPosition.z = -cameraDistance;
        mainCamera.transform.localPosition = mainCameraLocalPosition;
        rootsCamera.nearClipPlane = cameraDistance;
    }
}
