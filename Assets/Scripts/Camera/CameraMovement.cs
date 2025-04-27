using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform cameraLookAt;
    [SerializeField] float rotationSpeed;

    bool IsWallRiding { get => player.gameObject.GetComponent<WallRide>().isWallRiding; }
    bool IsOnSpline { get => player.gameObject.GetComponent<SplineRide>().isOnSpline; }

    void Update()
    {
        Vector3 dir = player.position - transform.position;
        cameraLookAt.forward = dir.normalized;
    }

    public void MoveCamera(Vector3 inputDir)
    {
        if (inputDir != Vector3.zero && !IsWallRiding && !IsOnSpline) player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }
}
