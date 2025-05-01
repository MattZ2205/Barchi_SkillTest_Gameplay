using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : Character
{
    [Header("Movement")]
    [SerializeField] float walkMaxSpeed;
    [SerializeField] float maxSpeedRun;
    [SerializeField] float movementDrag;
    [Header("Jump")]
    [SerializeField] float maxJumpHeight;
    [SerializeField] float jumpForce;

    WallRide WR;
    SplineRide SR;
    float maxSpeed;

    public bool IsWallRiding { get => WR.isWallRiding; }

    protected override void Start()
    {
        base.Start();
        WR = GetComponent<WallRide>();
        SR = GetComponent<SplineRide>();
    }

    public void Jump()
    {
        if (!SR.isOnSpline)
        {
            if (IsGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (IsWallRiding)
            {
                Vector3 jumpDir = (WR.wallNormal + Vector3.up).normalized;
                rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void Sprint(bool isSprinting)
    {
        if (!IsWallRiding && !SR.isOnSpline) maxSpeed = isSprinting ? maxSpeedRun : walkMaxSpeed;
    }

    public void Move(Vector3 inputDir)
    {
        if (inputDir != Vector3.zero)
        {
            rb.velocity =
                Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed) * Vector3.right +
                Mathf.Clamp(rb.velocity.y, -maxJumpHeight, maxJumpHeight) * Vector3.up +
                Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed) * Vector3.forward;
        }
        else
        {
            rb.velocity = new Vector3(
                Mathf.Max(0, rb.velocity.x - movementDrag),
                rb.velocity.y,
                Mathf.Max(0, rb.velocity.z - movementDrag));
        }

        if (!IsWallRiding && !SR.isOnSpline)
        {
            rb.AddForce(inputDir * speed, ForceMode.Force);
        }
    }
}