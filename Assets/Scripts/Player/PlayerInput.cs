using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] CameraMovement CM;
    [SerializeField] Transform cameraDir;

    PlayerMovement PM;
    Vector3 inputDir;

    void Start()
    {
        PM = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1)) PM.Jump();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button0)) PM.Sprint(true);
        else PM.Sprint(false);
    }

    void FixedUpdate()
    {
        inputDir = cameraDir.forward * Input.GetAxis("Vertical") + cameraDir.right * Input.GetAxis("Horizontal");
        inputDir.y = 0;

        PM.Move(inputDir.normalized);
        CM.MoveCamera(inputDir.normalized);
    }
}
